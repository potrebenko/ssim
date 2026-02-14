using FluentAssertions;

namespace SSIM.Parser.Tests;

public class SSIMParserTests
{
    private const string HeaderData =
        "1AIRLINE STANDARD SCHEDULE DATA SET     2                                                                                                                                                      002    000001";

    private const string CarrierData =
        "2LXX      S02 09JUN0510JUN0511JAN24SAS IATA DRAFT S90           09JUN05PABC1234/05APR                      XLAST SSM REFLECTED 02145001                                    1/  7/ 12/  6/  7ET0914000002";

    private const string FlightLegData =
        "3AXX F 410102F09JUN0510JUN051 3 5 72RUN21302130+04003 DZA22402240+03003 D92F008C038BQV145       N   LSL L     AB BC DE DIS     2AB AB AB KL 01232Z 2X  A  Z     S           F014Y119VVT3M33     UL000003";

    private const string SegmentRecordData =
        "4XAF 43310101J             AXC 20FRALHRAF TEST                                                                                                                                                    000004";

    private const string TrailerData =
        "5 XX 09JUN05                                                                                                                                                                               000515E000516";

    private static byte[] MakeBufferRecord(string data)
    {
        var record = new byte[Constants.RecordLength];
        Array.Fill(record, (byte)' ');
        var bytes = data.ToBytes();
        Array.Copy(bytes, record, Math.Min(bytes.Length, Constants.RecordLength - 1));
        record[Constants.RecordLength - 1] = (byte)'\n';
        return record;
    }

    [Fact]
    public void Parse_WithNoDataRecords_ShouldProduceValidJsonStructure()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(HeaderData.ToBytes(), CarrierData.ToBytes());
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().StartWith("[{");
        json.Should().EndWith("}]");
        json.Should().Contain("\"RecordType\":\"1\"");
        json.Should().Contain("\"RecordType\":\"2\"");
    }

    [Fact]
    public void Parse_ShouldDisposeReader()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(HeaderData.ToBytes(), CarrierData.ToBytes());
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        reader.DisposeCalled.Should().BeTrue();
    }

    [Fact]
    public void Parse_ShouldDisposeWriter()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(HeaderData.ToBytes(), CarrierData.ToBytes());
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        writer.DisposeCalled.Should().BeTrue();
    }

    [Fact]
    public void Parse_ShouldFlushComposer()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(HeaderData.ToBytes(), CarrierData.ToBytes());
        var writer = new FakeFileWriter();
        bool flushed = false;
        composer.OnFlush += (_, _) => flushed = true;
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        flushed.Should().BeTrue();
    }

    [Fact]
    public void Parse_WithType3Record_ShouldIncludeFlightLegData()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(
            HeaderData.ToBytes(),
            CarrierData.ToBytes(),
            MakeBufferRecord(FlightLegData));
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().Contain("\"RecordType\":\"3\"");
        json.Should().Contain("\"DepartureStation\":\"RUN\"");
        json.Should().Contain("\"ArrivalStation\":\"DZA\"");
    }

    [Fact]
    public void Parse_WithType4Record_ShouldIncludeSegmentData()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(
            HeaderData.ToBytes(),
            CarrierData.ToBytes(),
            MakeBufferRecord(SegmentRecordData));
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().Contain("\"RecordType\":\"4\"");
        json.Should().Contain("\"BoardPoint\":\"FRA\"");
        json.Should().Contain("\"OffPoint\":\"LHR\"");
    }

    [Fact]
    public void Parse_WithType5Record_ShouldIncludeTrailerData()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var reader = new FakeSSIMReader(
            HeaderData.ToBytes(),
            CarrierData.ToBytes(),
            MakeBufferRecord(TrailerData));
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().Contain("\"RecordType\":\"5\"");
        json.Should().Contain("\"AirlineDesignator\":\"XX\"");
    }

    [Fact]
    public void Parse_WithUnknownRecordType_ShouldSkipRecord()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var unknownRecord = "9" + new string(' ', 199);
        var reader = new FakeSSIMReader(
            HeaderData.ToBytes(),
            CarrierData.ToBytes(),
            MakeBufferRecord(unknownRecord));
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().NotContain("\"RecordType\":\"9\"");
        json.Should().Contain("\"RecordType\":\"1\"");
        json.Should().Contain("\"RecordType\":\"2\"");
    }

    [Fact]
    public void Parse_WithMultipleRecordsInBuffer_ShouldParseAll()
    {
        // Arrange
        var composer = new JsonComposer(16384);
        var flightRecord = MakeBufferRecord(FlightLegData);
        var trailerRecord = MakeBufferRecord(TrailerData);
        var combined = new byte[flightRecord.Length + trailerRecord.Length];
        Array.Copy(flightRecord, 0, combined, 0, flightRecord.Length);
        Array.Copy(trailerRecord, 0, combined, flightRecord.Length, trailerRecord.Length);

        var reader = new FakeSSIMReader(
            HeaderData.ToBytes(),
            CarrierData.ToBytes(),
            combined);
        var writer = new FakeFileWriter();
        var parser = new SSIMParser(reader, writer, composer);

        // Act
        parser.Parse();

        // Assert
        var json = composer.ToString();
        json.Should().Contain("\"RecordType\":\"3\"");
        json.Should().Contain("\"RecordType\":\"5\"");
    }

    private class FakeSSIMReader : ISSIMReader
    {
        private readonly byte[][] _headers;
        private readonly Queue<byte[]> _reads;

        public bool DisposeCalled { get; private set; }

        public FakeSSIMReader(byte[] header, byte[] carrier, params byte[][] reads)
        {
            _headers = [header, carrier];
            _reads = new Queue<byte[]>(reads);
        }

        public byte[][] ReadHeaders() => _headers;

        public void ReadRecord(byte[] buffer, int offset, int length, out int readLength)
        {
            if (_reads.Count == 0)
            {
                readLength = 0;
                return;
            }

            var data = _reads.Dequeue();
            Array.Copy(data, 0, buffer, offset, data.Length);
            readLength = data.Length;
        }

        public void Dispose() => DisposeCalled = true;
    }

    private class FakeFileWriter : IFileWriter
    {
        public bool DisposeCalled { get; private set; }
        public void Dispose() => DisposeCalled = true;
    }
}
