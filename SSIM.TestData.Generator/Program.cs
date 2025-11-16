using System.CommandLine;

namespace SSIM.TestData.Generator;

class Program
{
    static void Main(string[] args)
    {
        Option<int> recordsOption = new("--records", "-r")
        {
            DefaultValueFactory = x => 100,
            Description = "The number of records to generate."
        };
        Option<string> outputOption = new("--output", "-o")
        {
            DefaultValueFactory = x => "sample.ssim",
            Description = "The path to the output file."
        };

        var rootCommand = new RootCommand
        {
            recordsOption,
            outputOption
        };

        rootCommand.SetAction(resultParser =>
        {
            var recordsToGenerate = resultParser.GetRequiredValue(recordsOption);
            var outputPath = resultParser.GetRequiredValue(outputOption);
            ParseSSIM(recordsToGenerate, outputPath);
        });
        
        var resultParser = rootCommand.Parse(args);
        resultParser.Invoke();
    }

    private static void ParseSSIM(int recordsToGenerate, string outputPath)
    {
        var recordGenerator = new SSIMRecordGenerator();
        using var writer = new FileRecordWriter(outputPath);
        var generator = new SSIMGenerator(recordsToGenerate, recordGenerator, writer);
        generator.Generate();
        Console.WriteLine($"Generated {recordsToGenerate} records to {outputPath}");
    }
}