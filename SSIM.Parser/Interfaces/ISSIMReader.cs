namespace SSIM.Parser;

public interface ISSIMReader : IDisposable
{
    byte[][] ReadHeaders();
    void ReadRecord(byte[] buffer, int offset, int length, out int readLength);
}