using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace SSIM.Parser.Benchmark;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
//[SimpleJob(RuntimeMoniker.Net90)]
public class ParserBenchmark
{
    private readonly string _filePath = "g_sample.ssim";
  
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