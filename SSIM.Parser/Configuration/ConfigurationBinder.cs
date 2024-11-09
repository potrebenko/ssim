using System.CommandLine;
using System.CommandLine.Binding;

namespace SSIM.Parser;

public class ConfigurationBinder : BinderBase<Configuration>
{
    private readonly Option<int> _readBufferSize;
    private readonly Option<int> _writeBufferSize;
    private readonly Option<int> _jsonBufferSize;
    private readonly Option<string> _filePath;
    private readonly Option<string> _outputPath;

    public ConfigurationBinder(Option<int> readBufferSize, Option<int> writeBufferSize, Option<int> jsonBufferSize, 
        Option<string> filePath, Option<string> outputPath)
    {
        _readBufferSize = readBufferSize;
        _writeBufferSize = writeBufferSize;
        _jsonBufferSize = jsonBufferSize;
        _filePath = filePath;
        _outputPath = outputPath;
    }
    
    protected override Configuration GetBoundValue(BindingContext bindingContext)
    {
        return new Configuration
        {
            FileReaderBufferSize = bindingContext.ParseResult.GetValueForOption(_readBufferSize),
            FileWriterBufferSize = bindingContext.ParseResult.GetValueForOption(_writeBufferSize),
            JsonComposerBufferSize = bindingContext.ParseResult.GetValueForOption(_jsonBufferSize),
            FilePath = bindingContext.ParseResult.GetValueForOption(_filePath),
            OutputPath = bindingContext.ParseResult.GetValueForOption(_outputPath)
        };
    }
}