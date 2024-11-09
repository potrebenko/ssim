using BenchmarkDotNet.Running;

namespace SSIM.Parser.Benchmark;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<ParserBenchmark>();
    }
}