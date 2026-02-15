using FluentAssertions;

namespace SSIM.Parser.Tests;

public class SegmentDataRecordTests
{
    private const string SegmentInput =
        "4XAF 43310101J             AXC 20FRALHRAF TEST                                                                                                                                                    000004";

    [Fact]
    public void ParseSegmentData_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(2024);
        var input = SegmentInput.ToBytes();

        var expected = ",{\"RecordType\":\"4\",\"OperationalSuffix\":\"X\"," +
                       "\"FlightDesignator\":\"AF 4331\",\"AirlineDesignator\":\"AF\"," +
                       "\"FlightNumber\":\"4331\",\"ItineraryVariationIdentifier\":\"01\"," +
                       "\"LegSequenceNumber\":\"01\",\"ServiceType\":\"J\"," +
                       "\"ItineraryVariationIdentifierOverflow\":\"A\",\"BoardPointIndicator\":\"X\"," +
                       "\"OffPointIndicator\":\"C\",\"DataElementIdentifier\":\"20\"," +
                       "\"Segment\":\"FRALHR\",\"BoardPoint\":\"FRA\",\"OffPoint\":\"LHR\",\"Data\":\"AF TEST                                                                                                                                                    \"," +
                       "\"RecordSerialNumber\":\"000004\"}";

        // Act
        input.ParseSegmentDataRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }

    [Fact]
    public void ParseSegmentData_WithNonZeroOffset_ShouldParseCorrectly()
    {
        // Arrange
        var dataComposer = new JsonComposer(2024);
        var recordBytes = SegmentInput.ToBytes();
        var offset = Constants.RecordLength;
        var buffer = new byte[offset + recordBytes.Length + 100];
        Array.Copy(recordBytes, 0, buffer, offset, recordBytes.Length);

        // Act
        buffer.ParseSegmentDataRecord(offset, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Contain("\"RecordType\":\"4\"");
        str.Should().Contain("\"BoardPoint\":\"FRA\"");
        str.Should().Contain("\"OffPoint\":\"LHR\"");
        str.Should().Contain("\"Segment\":\"FRALHR\"");
    }

    [Fact]
    public void ParseSegmentData_ShouldExtractBoardAndOffPointFromSegment()
    {
        // Arrange - BoardPoint and OffPoint are derived from the same segment field
        var dataComposer = new JsonComposer(2024);
        var input = SegmentInput.ToBytes();

        // Act
        input.ParseSegmentDataRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Contain("\"Segment\":\"FRALHR\"");
        str.Should().Contain("\"BoardPoint\":\"FRA\"");
        str.Should().Contain("\"OffPoint\":\"LHR\"");
    }

    [Fact]
    public void ParseSegmentData_RecordSerialNumber_ShouldNotHaveTrailingComma()
    {
        // Arrange
        var dataComposer = new JsonComposer(2024);
        var input = SegmentInput.ToBytes();

        // Act
        input.ParseSegmentDataRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().EndWith("\"RecordSerialNumber\":\"000004\"}");
    }
}