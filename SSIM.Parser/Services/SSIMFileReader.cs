// ReSharper disable MustUseReturnValue
namespace SSIM.Parser;

public class SSIMFileReader : ISSIMReader
{
    private readonly FileStream _reader;
    private readonly byte[][] _headBufferSegments;

    public SSIMFileReader(string path, int fileReaderBufferSize)
    {
        _reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, fileReaderBufferSize, 
            FileOptions.SequentialScan);
        
        _headBufferSegments = new byte[2][];
        for (int i = 0; i < 2; i++)
        {
            _headBufferSegments[i] = new byte[Constants.RecordLength];
        }
    }

    public byte[][] ReadHeaders()
    {
        _reader.Read(_headBufferSegments[0], 0, Constants.RecordLength);

        _reader.Seek(1005, SeekOrigin.Begin);
        _reader.Read(_headBufferSegments[1], 0, Constants.RecordLength);

        _reader.Position = 2010;
        return _headBufferSegments;
    }

    public void ReadRecord(byte[] buffer, int offset, int length, out int readLength)
    {
        var readBytes = _reader.Read(buffer, offset, length);
        readLength = readBytes;
    }
    
    public void Dispose()
    {
        _reader.Close();
        _reader.Dispose();
    }
}