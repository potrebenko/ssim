using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace SSIM.Parser.Benchmark;

[MemoryDiagnoser]
// Uncomment the following lines to run benchmarks for .NET 8.0 and .NET 9.0
//[ShortRunJob]
//[SimpleJob(RuntimeMoniker.Net80)]
//[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class ParserBenchmark
{
    private readonly string _filePath = "sample.ssim";

    [Benchmark]
    public void ParseSchedule()
    {
        var reader = new SSIMFileReader(_filePath, 20_100);
        var composer = new JsonComposer(16_384);
        var writer = new FileWriter("SSIM.json", composer, 16_384);
        var parser = new SSIMParser(reader, writer, composer);
        var count = parser.Parse();
    }
}