using System.Text;
using FluentAssertions;

namespace SSIM.Parser.Tests;

public class SSIMFileReaderTests : IDisposable
{
    private readonly string _tempDir;

    public SSIMFileReaderTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"ssim_tests_{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempDir);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, true);
    }

    private string CreateSsimFile(params string[] records)
    {
        var path = Path.Combine(_tempDir, "test.ssim");
        // Each record must be exactly RecordLength bytes (200 data + newline)
        using var fs = new FileStream(path, FileMode.Create);
        foreach (var record in records)
        {
            var padded = record.PadRight(Constants.RecordLength - 1);
            var bytes = Encoding.ASCII.GetBytes(padded);
            fs.Write(bytes, 0, Constants.RecordLength - 1);
            fs.WriteByte((byte)'\n');
        }
        return path;
    }

    [Fact]
    public void Constructor_WithNonExistentPath_ShouldThrowFileNotFoundException()
    {
        // Act
        var act = () => new SSIMFileReader("/nonexistent/path/to/file.ssim", 4096);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void Constructor_WithExistingPath_ShouldNotThrow()
    {
        // Arrange
        var header = "1AIRLINE STANDARD SCHEDULE DATA SET     2                                                                                                                                                      002    000001";
        var carrier = "2LXX      S02 09JUN0510JUN0511JAN24SAS IATA DRAFT S90           09JUN05PABC1234/05APR                      XLAST SSM REFLECTED 02145001                                    1/  7/ 12/  6/  7ET0914000002";
        var path = CreateSsimFile(header, header, header, header, header, carrier);

        // Act & Assert
        var act = () =>
        {
            var reader = new SSIMFileReader(path, 4096);
            reader.Dispose();
        };
        act.Should().NotThrow();
    }

    [Fact]
    public void ReadHeaders_ShouldReturnTwoRecords()
    {
        // Arrange - SSIM files: record 1 at byte 0, 5 records padding, record 2 at byte 1005
        // Create a file with enough data for ReadHeaders to work
        var path = Path.Combine(_tempDir, "headers.ssim");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            // Write enough bytes for ReadHeaders: needs at least 2010 + RecordLength bytes
            var totalSize = 2010 + Constants.RecordLength;
            var data = new byte[totalSize];

            // Record type 1 at position 0
            var header1 = Encoding.ASCII.GetBytes("1AIRLINE STANDARD SCHEDULE DATA SET     2");
            Array.Copy(header1, data, header1.Length);

            // Record type 2 at position 1005
            var header2 = Encoding.ASCII.GetBytes("2LXX");
            Array.Copy(header2, 0, data, 1005, header2.Length);

            fs.Write(data, 0, data.Length);
        }

        var reader = new SSIMFileReader(path, 4096);

        // Act
        var headers = reader.ReadHeaders();

        // Assert
        headers.Should().HaveCount(2);
        headers[0].Should().HaveCount(Constants.RecordLength);
        headers[1].Should().HaveCount(Constants.RecordLength);
        headers[0][0].Should().Be((byte)'1');
        headers[1][0].Should().Be((byte)'2');

        reader.Dispose();
    }

    [Fact]
    public void ReadRecord_ShouldReadDataFromFile()
    {
        // Arrange
        var path = Path.Combine(_tempDir, "readrecord.ssim");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            // Write header area (2010 bytes) + one data record
            var headerArea = new byte[2010];
            fs.Write(headerArea, 0, headerArea.Length);

            var recordData = Encoding.ASCII.GetBytes("3AXX");
            var recordPadding = new byte[Constants.RecordLength - recordData.Length];
            fs.Write(recordData, 0, recordData.Length);
            fs.Write(recordPadding, 0, recordPadding.Length);
        }

        var reader = new SSIMFileReader(path, 4096);
        reader.ReadHeaders(); // Advances position to 2010

        // Act
        var buffer = new byte[Constants.RecordLength];
        reader.ReadRecord(buffer, 0, Constants.RecordLength, out var readLength);

        // Assert
        readLength.Should().Be(Constants.RecordLength);
        buffer[0].Should().Be((byte)'3');
        buffer[1].Should().Be((byte)'A');
        buffer[2].Should().Be((byte)'X');
        buffer[3].Should().Be((byte)'X');

        reader.Dispose();
    }

    [Fact]
    public void ReadRecord_AtEndOfFile_ShouldReturnZeroReadLength()
    {
        // Arrange
        var path = Path.Combine(_tempDir, "eof.ssim");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            // Write just enough for ReadHeaders (2010 + RecordLength) with no data records
            var data = new byte[2010 + Constants.RecordLength];
            fs.Write(data, 0, data.Length);
        }

        var reader = new SSIMFileReader(path, 4096);
        reader.ReadHeaders();

        // Read past the remaining data after headers
        var buffer = new byte[20100];
        reader.ReadRecord(buffer, 0, buffer.Length, out var readLength1);

        // Act - second read should be at EOF
        reader.ReadRecord(buffer, 0, buffer.Length, out var readLength2);

        // Assert
        readLength2.Should().Be(0);

        reader.Dispose();
    }

    [Fact]
    public void Dispose_ShouldCloseFileHandle()
    {
        // Arrange
        var path = Path.Combine(_tempDir, "dispose.ssim");
        File.WriteAllBytes(path, new byte[2010 + Constants.RecordLength]);
        var reader = new SSIMFileReader(path, 4096);

        // Act
        reader.Dispose();

        // Assert - file should be released and deletable
        var act = () => File.Delete(path);
        act.Should().NotThrow();
    }
}