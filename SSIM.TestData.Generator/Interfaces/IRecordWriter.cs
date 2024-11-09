namespace SSIM.TestData.Generator;

public interface IRecordWriter : IDisposable
{
    void Write(string record);
    void CloseAndFlush();
}

public class FileRecordWriter : IRecordWriter, IAsyncDisposable
{
    private readonly StreamWriter _streamWriter;
    public FileRecordWriter(string pathToFile)
    {
        _streamWriter = new StreamWriter(new FileStream(pathToFile, FileMode.Create));
    }
    
    public void Write(string record)
    {
        _streamWriter.WriteLine(record);
    }

    public void CloseAndFlush()
    {
        _streamWriter.Close();
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _streamWriter.DisposeAsync();
    }
}