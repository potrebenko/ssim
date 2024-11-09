namespace SSIM.Parser;

public class Configuration
{
    public int FileReaderBufferSize { get; set; }
    public int FileWriterBufferSize { get; set; }
    public int JsonComposerBufferSize { get; set; }
    public string FilePath { get; set; }
    public string OutputPath { get; set; }
}