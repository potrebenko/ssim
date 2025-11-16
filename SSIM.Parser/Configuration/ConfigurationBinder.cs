using System.CommandLine;
using System.CommandLine.Binding;

namespace SSIM.Parser;

public class ConfigurationBinder(
    Option<int> readBufferSize,
    Option<int> writeBufferSize,
    Option<int> jsonBufferSize,
    Option<string> filePath,
    Option<string> outputPath)
{
    public Configuration GetBoundValue(ParseResult parseResult)
    {
        return new Configuration
        {
            FileReaderBufferSize = parseResult.GetRequiredValue(readBufferSize),
            FileWriterBufferSize = parseResult.GetRequiredValue(writeBufferSize),
            JsonComposerBufferSize = parseResult.GetRequiredValue(jsonBufferSize),
            FilePath = parseResult.GetRequiredValue(filePath),
            OutputPath = parseResult.GetRequiredValue(outputPath)
        };
    }
}