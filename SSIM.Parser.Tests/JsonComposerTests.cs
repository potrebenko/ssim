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
        byte[] value = "name".ToBytes();

        // Act
        jsonComposer.AppendField(value, value, true);
        jsonComposer.AppendField(value, value, false);

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
}