namespace SSIM.Parser;

public interface IDataComposer
{
    event Action<byte[], int> OnFlush;
    void AppendCloseBracket();
    void AppendOpenBracket();
    void AppendComma();
    void AppendCloseCurlyBrace();
    void AppendOpenCurlyBrace();
    void AppendField(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength, bool setComma = true);
    void AppendField(ReadOnlySpan<byte> fieldName, ReadOnlySpan<byte> record, bool setComma = true);
    void Flush();
    void AppendFieldRightJustified(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true);
    void AppendFieldLeftJustified(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true);

    void CheckBuffer(int size);
}