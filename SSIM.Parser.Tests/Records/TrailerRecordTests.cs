using FluentAssertions;

namespace SSIM.Parser.Tests;

public class TrailerRecordTests
{
    private const string TrailerInput =
        "5 XX 09JUN05                                                                                                                                                                               000515E000516";

    [Fact]
    public void ParseTrailer_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var input = TrailerInput.ToBytes();
        var expected = ",{\"RecordType\":\"5\",\"AirlineDesignator\":\"XX\"," +
                       "\"ReleaseSellDate\":\"09JUN05\",\"SerialNumberCheckReference\":\"000515\"," +
                       "\"ContinuationEndCode\":\"E\",\"RecordSerialNumber\":\"000516\"}";

        // Act
        input.ParseTrailerRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }

    [Fact]
    public void ParseTrailer_WithNonZeroOffset_ShouldParseCorrectly()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var recordBytes = TrailerInput.ToBytes();
        var offset = Constants.RecordLength;
        var buffer = new byte[offset + recordBytes.Length + 100];
        Array.Copy(recordBytes, 0, buffer, offset, recordBytes.Length);

        // Act
        buffer.ParseTrailerRecord(offset, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Contain("\"RecordType\":\"5\"");
        str.Should().Contain("\"AirlineDesignator\":\"XX\"");
        str.Should().Contain("\"ReleaseSellDate\":\"09JUN05\"");
        str.Should().Contain("\"ContinuationEndCode\":\"E\"");
    }

    [Fact]
    public void ParseTrailer_ShouldLeftTrimAirlineDesignator()
    {
        // Arrange - "XX " should be trimmed to "XX" by AppendFieldLeftJustified
        var dataComposer = new JsonComposer(1024);
        var input = TrailerInput.ToBytes();

        // Act
        input.ParseTrailerRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Contain("\"AirlineDesignator\":\"XX\"");
    }

    [Fact]
    public void ParseTrailer_RecordSerialNumber_ShouldNotHaveTrailingComma()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var input = TrailerInput.ToBytes();

        // Act
        input.ParseTrailerRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().EndWith("\"RecordSerialNumber\":\"000516\"}");
    }
}