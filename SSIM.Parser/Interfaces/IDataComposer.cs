namespace SSIM.Parser;

public interface IDataComposer
{
    event EventHandler<(byte[] buffer, int position)> OnFlush;
    void AppendCloseBracket();
    void AppendOpenBracket();
    void AppendComma();
    void AppendCloseCurlyBrace();
    void AppendOpenCurlyBrace();
    void AppendField(byte[] fieldName, byte[] record, int offset, int valueLength, bool setComma = true);
    void AppendField(byte[] fieldName, byte[] fieldValue, bool setComma = true);
    void Flush();
    void AppendFieldRightJustified(byte[] fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true);
    void AppendFieldLeftJustified(byte[] fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true);
}