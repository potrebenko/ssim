using AutoFixture.Xunit2;
using FluentAssertions;
using System.Text;

namespace SSIM.Parser.Tests;

public class JsonComposerTests
{
    [Theory, AutoData]
    public void AppendComma_CommaShouldBeAppended(JsonComposer jsonComposer)
    {
        // Act
        jsonComposer.AppendComma();

        // Assert
        jsonComposer.InternalBuffer[0].Should().Be((byte)',');
    }

    [Theory, AutoData]
    public void AppendOpenBracket_OpenBracketShouldBeAppended(JsonComposer jsonComposer)
    {
        // Act
        jsonComposer.AppendOpenBracket();

        // Assert
        jsonComposer.InternalBuffer[0].Should().Be((byte)'[');
    }

    [Theory, AutoData]
    public void AppendCloseBracket_CloseBracketShouldBeAppended(JsonComposer jsonComposer)
    {
        // Act
        jsonComposer.AppendCloseBracket();

        // Assert
        jsonComposer.InternalBuffer[0].Should().Be((byte)']');
    }

    [Theory, AutoData]
    public void AppendOpenCurlyBrace_OpenCurlyBraceShouldBeAppended(JsonComposer jsonComposer)
    {
        // Act
        jsonComposer.AppendOpenCurlyBrace();

        // Assert
        jsonComposer.InternalBuffer[0].Should().Be((byte)'{');
    }

    [Theory, AutoData]
    public void AppendCloseCurlyBrace_CloseCurlyBraceShouldBeAppended(JsonComposer jsonComposer)
    {
        // Act
        jsonComposer.AppendCloseCurlyBrace();

        // Assert
        jsonComposer.InternalBuffer[0].Should().Be((byte)'}');
    }

    [Theory, AutoData]
    public void AppendField_FieldShouldBeAppended(JsonComposer jsonComposer)
    {
        // Arrange
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "value".ToBytes();
        string expected = "\"name\":\"value\",";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue);

        // Assert
        string actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Theory, AutoData]
    public void AppendField_WithOffset_FieldShouldBeAppended(JsonComposer jsonComposer)
    {
        // Arrange
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "value".ToBytes();
        string expected = "\"name\":\"value\",";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue, 0, fieldValue.Length);

        // Assert
        string actual =
            Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Theory, AutoData]
    public void Flush_ShouldInvokeOnFlushEvent(JsonComposer jsonComposer)
    {
        // Arrange
        bool eventInvoked = false;
        jsonComposer.OnFlush += (sender, args) => eventInvoked = true;

        // Act
        jsonComposer.Flush();

        // Assert
        eventInvoked.Should().BeTrue();
    }

    [Fact]
    public void AppendField_WithOffsetAndLength_ShouldInvokeOnFlushEvent()
    {
        // Arrange
        bool eventInvoked = false;
        JsonComposer jsonComposer = new JsonComposer(15);
        jsonComposer.OnFlush += (sender, args) => eventInvoked = true;

        // Act
        jsonComposer.CheckBuffer(20);

        // Assert
        eventInvoked.Should().BeTrue();
    }
    
    [Theory, AutoData]
    public void AppendFieldRightJustified_ShouldRemoveLeadingBlanks(JsonComposer jsonComposer)
    {
        // Arrange
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "  001".ToBytes();
        string expected = "\"name\":\"001\",";

        // Act
        jsonComposer.AppendFieldRightJustified(fieldName, fieldValue, 0, fieldValue.Length);

        // Assert
        string actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    } 
    
    [Theory, AutoData]
    public void AppendFieldLeftJustified_ShouldRemoveTrailingBlanks(JsonComposer jsonComposer)
    {
        // Arrange
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "AAA  ".ToBytes();
        string expected = "\"name\":\"AAA\",";

        // Act
        jsonComposer.AppendFieldLeftJustified(fieldName, fieldValue, 0, fieldValue.Length);

        // Assert
        string actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Fact]
    public void AppendField_WithSetCommaFalse_ShouldNotAppendComma()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "value".ToBytes();
        string expected = "\"name\":\"value\"";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue, false);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
        jsonComposer.Position.Should().Be(expected.Length);
    }

    [Fact]
    public void AppendField_WithOffsetAndSetCommaFalse_ShouldNotAppendComma()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "value".ToBytes();
        string expected = "\"name\":\"value\"";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue, 0, fieldValue.Length, false);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
        jsonComposer.Position.Should().Be(expected.Length);
    }

    [Fact]
    public void AppendField_WithLongFieldName_ShouldUseCopyToPath()
    {
        // Arrange - field name > 8 bytes triggers Span.CopyTo path
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "LongFieldName".ToBytes();
        byte[] fieldValue = "val".ToBytes();
        string expected = "\"LongFieldName\":\"val\",";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Fact]
    public void AppendField_WithLongValue_ShouldUseCopyToPath()
    {
        // Arrange - value > 8 bytes triggers Span.CopyTo path
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "LongValueData".ToBytes();
        string expected = "\"name\":\"LongValueData\",";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Fact]
    public void AppendField_WithOffsetAndLongValue_ShouldUseBlockCopyPath()
    {
        // Arrange - value > 8 bytes triggers Buffer.BlockCopy path in offset overload
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = "LongValueData".ToBytes();
        string expected = "\"name\":\"LongValueData\",";

        // Act
        jsonComposer.AppendField(fieldName, fieldValue, 0, fieldValue.Length);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }

    [Fact]
    public void CheckBuffer_WhenSufficientSpace_ShouldNotFlush()
    {
        // Arrange
        bool flushed = false;
        var jsonComposer = new JsonComposer(1024);
        jsonComposer.OnFlush += (_, _) => flushed = true;

        // Act
        jsonComposer.CheckBuffer(100);

        // Assert
        flushed.Should().BeFalse();
    }

    [Fact]
    public void CheckBuffer_ExactlyAtLimit_ShouldFlush()
    {
        // Arrange
        bool flushed = false;
        var jsonComposer = new JsonComposer(100);
        jsonComposer.OnFlush += (_, _) => flushed = true;

        // Act - position(0) + size(100) >= bufferSize(100)
        jsonComposer.CheckBuffer(100);

        // Assert
        flushed.Should().BeTrue();
    }

    [Fact]
    public void CheckBuffer_WhenFlushTriggered_ShouldResetPosition()
    {
        // Arrange
        var jsonComposer = new JsonComposer(50);
        jsonComposer.OnFlush += (_, _) => { };
        jsonComposer.AppendOpenBracket(); // position = 1

        // Act
        jsonComposer.CheckBuffer(50); // 1 + 50 >= 50 → flush

        // Assert
        jsonComposer.Position.Should().Be(0);
    }

    [Fact]
    public void Position_AfterMultipleAppends_ShouldTrackTotalBytesWritten()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);

        // Act
        jsonComposer.AppendOpenBracket();    // 1 byte
        jsonComposer.AppendOpenCurlyBrace(); // 1 byte
        jsonComposer.AppendCloseCurlyBrace();// 1 byte
        jsonComposer.AppendComma();          // 1 byte
        jsonComposer.AppendCloseBracket();   // 1 byte

        // Assert
        jsonComposer.Position.Should().Be(5);
    }

    [Fact]
    public void ToString_ShouldReturnWrittenContent()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);
        jsonComposer.AppendOpenBracket();
        jsonComposer.AppendCloseBracket();

        // Act
        var result = jsonComposer.ToString();

        // Assert
        result.Should().Be("[]");
    }

    [Fact]
    public void Flush_ShouldPassCurrentBufferAndPosition()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);
        jsonComposer.AppendOpenBracket();
        jsonComposer.AppendCloseBracket();

        int capturedPosition = -1;
        jsonComposer.OnFlush += (buffer, position) =>
        {
            capturedPosition = position;
        };

        // Act
        jsonComposer.Flush();

        // Assert
        capturedPosition.Should().Be(2);
    }

    [Fact]
    public void AppendFieldRightJustified_SingleLeadingBlank_ShouldTrimIt()
    {
        // Arrange
        var jsonComposer = new JsonComposer(1024);
        byte[] fieldName = "name".ToBytes();
        byte[] fieldValue = " XY".ToBytes();
        string expected = "\"name\":\"XY\",";

        // Act
        jsonComposer.AppendFieldRightJustified(fieldName, fieldValue, 0, fieldValue.Length);

        // Assert
        var actual = Encoding.ASCII.GetString(jsonComposer.InternalBuffer.ToArray(), 0, expected.Length);
        actual.Should().Be(expected);
    }
}