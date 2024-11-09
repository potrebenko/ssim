using System.CommandLine;

namespace SSIM.TestData.Generator;

class Program
{
    static void Main(string[] args)
    {
        var recordsOption = new Option<int>(
            aliases: ["-r", "--records"],
            getDefaultValue: () => 100,
            description: "The number of records to generate.");
        var outputOption = new Option<string>(
            aliases: ["-o", "--output"],
            getDefaultValue: () => "sample.ssim",
            description: "The path to the output file.");

        var rootCommand = new RootCommand();
        rootCommand.AddOption(recordsOption);
        rootCommand.AddOption(outputOption);
        
        rootCommand.SetHandler((recordsToGenerate, outputPath) =>
        {
            var recordGenerator = new SSIMRecordGenerator();
            using var writer = new FileRecordWriter(outputPath);
            var generator = new SSIMGenerator(recordsToGenerate, recordGenerator, writer);
            generator.Generate();
            Console.WriteLine($"Generated {recordsToGenerate} records to {outputPath}");
        }, recordsOption, outputOption);
        rootCommand.Invoke(args);
    }
}