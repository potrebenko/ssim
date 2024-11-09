using FluentAssertions;

namespace SSIM.Parser.Tests;

public class HeaderRecordTests
{
    [Fact]
    public void ParseHeader_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var input =
"1AIRLINE STANDARD SCHEDULE DATA SET     2                                                                                                                                                      002    000001"
                .ToBytes();
        var expected =
            "{\"RecordType\":\"1\",\"TitleOfContents\":\"AIRLINE STANDARD SCHEDULE DATA SET\"," +
            "\"NumberOfSeasons\":\"2\",\"DataSetSerialNumber\":\"002\"," +
            "\"RecordSerialNumber\":\"000001\"}";

        // Act
        input.ParseHeaderRecord(dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }
}