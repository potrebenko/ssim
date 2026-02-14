using FluentAssertions;

namespace SSIM.Parser.Tests;

public class SSIMFileReaderTests
{
    [Fact]
    public void Constructor_WithNonExistentPath_ShouldThrowFileNotFoundException()
    {
        // Act
        var act = () => new SSIMFileReader("/nonexistent/path/to/file.ssim", 4096);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }
}