using FluentAssertions;

namespace SSIM.Parser.Tests;

public class TrailerRecordTests
{ 
    [Fact]
    public void ParseTrailer_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var input = 
            "5 XX 09JUN05                                                                                                                                                                               000515E000516"
            .ToBytes();
        var expected = ",{\"RecordType\":\"5\",\"AirlineDesignator\":\"XX\"," +
                       "\"ReleaseSellDate\":\"09JUN05\",\"SerialNumberCheckReference\":\"000515\"," +
                       "\"ContinuationEndCode\":\"E\",\"RecordSerialNumber\":\"000516\"}";

        // Act
        input.ParseTrailerRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }
}