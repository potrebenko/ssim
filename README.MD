![Logo](/assets/plane.png "SSIM parser")
```
1 000 000 records

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1 (24B83) [Darwin 24.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.108
  [Host]   : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
  .NET 8.0 : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD

Job=.NET 8.0  Runtime=.NET 8.0  

```
| Method        | Mean     | Error    | StdDev   | Allocated |
|-------------- |---------:|---------:|---------:|----------:|
| ParseSchedule | :zap: 886.4 ms | 16.48 ms | 21.42 ms |  74.84 KB |

```
1 000 000 records. Cold start
./SSIMParser -i g_sample.ssim -o SSIM_output.json
Parsed 999999 records
Elapsed time: :zap: 00:02:14.9240791

192 MB g_sample.ssim
1.3 GB SSIM_output.json
```
