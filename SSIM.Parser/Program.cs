using System.CommandLine;
using System.Diagnostics;

namespace SSIM.Parser;

class Program
{
    static void Main(string[] args)
    {
        var readBufferSize = new Option<int>(
            ["-r", "--read-buffer-size"],
            getDefaultValue: () => 20_100,
            description: "The buffer size for the file reader.");
        var writeBufferSize = new Option<int>(
            ["-w", "--write-buffer-size"],
            getDefaultValue: () => 16_384,
            description: "The buffer size for the file writer.");
        var jsonBufferSize = new Option<int>(
            ["-j", "--json-buffer-size"],
            getDefaultValue: () => 16_384,
            description: "The buffer size for the json composer.");
        var pathToFile = new Option<string>(
            ["-i", "--input-path"],
            getDefaultValue: () => "sample.ssim",
            description: "The path to the SSIM file.");
        var pathToOutput = new Option<string>(
            ["-o", "--output-path"],
            getDefaultValue: () => "SSIM_output.json",
            description: "The path to the output file.");
        
        var rootCommand = new RootCommand
        {
            readBufferSize,
            writeBufferSize,
            jsonBufferSize,
            pathToFile,
            pathToOutput
        };
        
        var configurationBinder = new ConfigurationBinder(
            readBufferSize, writeBufferSize, jsonBufferSize, pathToFile, pathToOutput);

        rootCommand.SetHandler(RunParser, configurationBinder);
            
        rootCommand.Invoke(args);
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