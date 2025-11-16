![Logo](/assets/plane.png "SSIM parser")

# Converter of SSIM format to JSON format

The SSIM format is a simple text-based format for describing the schedule of a flight. It is used by the United States Department of Transportation (DOT) for flight schedules. The format consists of a header section and a data section. The header section contains metadata about the schedule, such as the date and time of the schedule, the origin and destination airports, and the name of the aircraft. The data section contains the schedule data, which includes the departure and arrival times, the flight numbers, and the aircraft types.

### SSIMParser
The SSIMParser is high performance tool for parsing data from SSIM format to JSON format. It is a console application written in C# and uses the BenchmarkDotNet library for performance testing. The tool uses data-oriented design and is optimized for performance.

```
Usage:
  SSIMParser [options]

Options:
  -r, --read-buffer-size <read-buffer-size>  The buffer size for the file reader.
                                             [default: 20100]
  -w, --write-buffer-size                    The buffer size for the file writer.
  <write-buffer-size>                        [default: 16384]
  -j, --json-buffer-size <json-buffer-size>  The buffer size for the json composer.
                                             [default: 16384]
  -i, --input-path <input-path>              The path to the SSIM file. [default:
                                             sample.ssim]
  -o, --output-path <output-path>            The path to the output file. [default:
                                             SSIM_output.json]
  --version                                  Show version information
  -?, -h, --help                             Show help and usage information
```

### SSIMGenerator
SSIMGenerator is a tool for generating test data in SSIM format files.

```
Usage:
  SSIMGenerator [options]

Options:
  -r, --records <records>  The number of records to generate. [default: 100]
  -o, --output <output>    The path to the output file. [default: sample.ssim]
  --version                Show version information
  -?, -h, --help           Show help and usage information
```

### Performance tests
To measure the performance you have two ways:

1. Run SSIM.Parser.Benchmark project. This project uses the simple.ssim file from the SSIM.Parser project. If you are able to change the input file, replace it by a new one following these steps:
    - Add a new file to SSIM.Parser project
    - Set property to this file "Copy always"
    - Change the name of the file in SSIM.Parser.Benchmark to a new file
    - You can prepare the file to use SSIM.TestData.Generator to run it with arguments:

```
  > ./SSIMGenerator -r 999996 -o g_simple.ssim
```
2. Run SSIMParser. It uses the same simple.ssim file to parse. You can set the input file via command arguments.

> [!CAUTION]
> Be careful of big files. 1 000 000 records generates a 1.3 GB json file.

### Test results

#### .NET 8 macOS Sequoia 15.1

```
1 000 000 records with BenchmarksDotNet

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1 (24B83) [Darwin 24.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.108
  [Host]   : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
  .NET 8.0 : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD

Job=.NET 8.0  Runtime=.NET 8.0

| Method        | Mean     | Error    | StdDev   | Allocated |
|-------------- |---------:|---------:|---------:|----------:|
| ParseSchedule | 886.4 ms | 16.48 ms | 21.42 ms |  74.84 KB |
```

```
1 000 000 records. Cold start .NET 8

> ./SSIMParser -i sample.ssim -o SSIM_output.json

Parsed 999999 records
Elapsed time: 00:02:14.9240791

192 MB sample.ssim
1.3 GB SSIM_output.json
```

#### .NET 10 macOS Tahoe 26.1 (has some performance improvements)

```
1 000 000 records with BenchmarksDotNet

// * Summary *

BenchmarkDotNet v0.15.6, macOS 26.1 (25B78) [Darwin 25.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), Arm64 RyuJIT armv8.0-a
  .NET 10.0 : .NET 10.0.0 (10.0.0, 10.0.25.52411), Arm64 RyuJIT armv8.0-a
  .NET 8.0  : .NET 8.0.22 (8.0.22, 8.0.2225.52707), Arm64 RyuJIT armv8.0-a
  .NET 9.0  : .NET 9.0.11 (9.0.11, 9.0.1125.51716), Arm64 RyuJIT armv8.0-a


| Method        | Job       | Runtime   | Mean     | Error    | StdDev   | Allocated |
|-------------- |---------- |---------- |---------:|---------:|---------:|----------:|
| ParseSchedule | .NET 10.0 | .NET 10.0 | 814.7 ms | 16.11 ms | 19.79 ms |  74.79 KB |
| ParseSchedule | .NET 8.0  | .NET 8.0  | 822.4 ms | 12.20 ms | 11.42 ms |  74.73 KB |
| ParseSchedule | .NET 9.0  | .NET 9.0  | 810.5 ms | 11.74 ms | 10.98 ms |  74.74 KB |
```

```
1 000 000 records. Cold start .NET 10

> ./SSIMParser -i sample.ssim -o SSIM_output.json

Parsed 999999 records.
Elapsed time: 00:01:39.8150125

192 MB sample.ssim
1.3 GB SSIM_output.json
```