namespace SSIM.Parser;

public class FileWriter : IFileWriter
{
    private readonly IDataComposer _dataComposer;
    private readonly FileStream _streamWriter;
    
    public FileWriter(string path, IDataComposer dataComposer, int writeBufferSize)
    {
        _dataComposer = dataComposer;
        _dataComposer.OnFlush += OnFlush;
        _streamWriter = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, writeBufferSize,
            FileOptions.SequentialScan);
    }

    private void OnFlush(byte[] buffer, int position)
    {
        WriteBytes(buffer, position);
    }

    private void WriteBytes(byte[] buffer, int position)
    {
        _streamWriter.Write(buffer, 0, position);
    }

    public void Dispose()
    {
        _dataComposer.OnFlush -= OnFlush;
        _streamWriter.Flush();
        _streamWriter.Dispose();
    }
}