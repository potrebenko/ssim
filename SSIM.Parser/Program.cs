using System.CommandLine;
using System.Diagnostics;

namespace SSIM.Parser;

class Program
{
    static void Main(string[] args)
    {
        Option<int> readBufferSize = new("--read-buffer-size", "-r")
        {
            DefaultValueFactory = x => 20_100,
            Description = "The buffer size for the file reader."
        };
        Option<int> writeBufferSize = new("--write-buffer-size", "-w")
        {
            DefaultValueFactory = x => 16_384,
            Description = "The buffer size for the file writer."
        };
        Option<int> jsonBufferSize = new("--json-buffer-size", "-j")
        {
            DefaultValueFactory = x => 16_384,
            Description = "The buffer size for the json composer."
        };
        Option<string> pathToFile = new("--input-path", "-i")
        {
            DefaultValueFactory = x => "sample.ssim",
            Description = "The path to the SSIM file."
        };
        Option<string> pathToOutput = new("--output-path", "-o")
        {
            DefaultValueFactory = x => "SSIM_output.json",
            Description = "The path to the output file."
        };

        var rootCommand = new RootCommand
        {
            readBufferSize,
            writeBufferSize,
            jsonBufferSize,
            pathToFile,
            pathToOutput
        };

        var configurationBinder = new ConfigurationBinder(
            readBufferSize,
            writeBufferSize,
            jsonBufferSize,
            pathToFile,
            pathToOutput);

        rootCommand.SetAction(parseResult =>
        {
            var configuration = configurationBinder.GetBoundValue(parseResult);
            RunParser(configuration);
        });

        var parseResult = rootCommand.Parse(args);
        parseResult.Invoke();
    }

    private static void RunParser(Configuration configuration)
    {
        var startTime = Stopwatch.GetTimestamp();
        var reader = new SSIMFileReader(configuration.FilePath, configuration.FileReaderBufferSize);
        var composer = new JsonComposer(configuration.JsonComposerBufferSize);
        var writer = new FileWriter(configuration.OutputPath, composer, configuration.FileWriterBufferSize);
        var parser = new SSIMParser(reader, writer, composer);
        var records = parser.Parse();
        var endTime = Stopwatch.GetTimestamp();

        Console.WriteLine($"Parsed {records} records.");
        Console.WriteLine($"Elapsed time: {TimeSpan.FromTicks(endTime - startTime)}");
    }
}