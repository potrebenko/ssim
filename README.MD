![Logo](/assets/plane.png "SSIM parser")

# Converter of SSIM format to JSON format

The SSIM (Standard Schedules Information Manual) format is an IATA standard used by airlines worldwide to exchange schedule data. It is a fixed-width text format where each record is exactly 200 bytes plus a newline character. A file contains a header record (type 1), a carrier record (type 2), flight leg records (type 3), segment data records (type 4), and a trailer record (type 5).

### SSIMParser
SSIMParser is a high-performance tool for converting SSIM files to JSON. It is a console application written in C# that uses data-oriented design with byte-level operations to achieve minimal memory allocation (~75KB) even when processing 192MB input files into 1.3GB JSON output.

```
Usage:
  SSIMParser [options]

Options:
  -r, --read-buffer-size <read-buffer-size>    The buffer size for the file reader.
                                               [default: 20100]
  -w, --write-buffer-size <write-buffer-size>  The buffer size for the file writer.
                                               [default: 16384]
  -j, --json-buffer-size <json-buffer-size>    The buffer size for the json composer.
                                               [default: 16384]
  -i, --input-path <input-path>                The path to the SSIM file. [default:
                                               sample.ssim]
  -o, --output-path <output-path>              The path to the output file. [default:
                                               SSIM_output.json]
  --version                                    Show version information
  -?, -h, --help                               Show help and usage information
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

### Project structure

| Project | Target | Purpose |
|---|---|---|
| SSIM.Parser | net8.0; net9.0; net10.0 | Core library and CLI |
| SSIM.Parser.Tests | net10.0 | xUnit tests |
| SSIM.Parser.Benchmark | net8.0; net9.0; net10.0 | BenchmarkDotNet performance tests |
| SSIM.TestData.Generator | net10.0 | CLI to generate SSIM test files |

### Build and test

```bash
dotnet build SSIMParser.sln
dotnet test
```

### Performance tests
To measure the performance you have two ways:

1. Run SSIM.Parser.Benchmark project. This project uses the sample.ssim file from the SSIM.Parser project. If you are able to change the input file, replace it by a new one following these steps:
    - Add a new file to SSIM.Parser project
    - Set property to this file "Copy always"
    - Change the name of the file in SSIM.Parser.Benchmark to a new file
    - You can prepare the file to use SSIM.TestData.Generator to run it with arguments:

```
  > ./SSIMGenerator -r 999996 -o sample.ssim
```
2. Run SSIMParser. It uses the same sample.ssim file to parse. You can set the input file via command arguments.

> [!CAUTION]
> Be careful of big files. 1 000 000 records generates a 1.3 GB json file.

### Test results

#### .NET 10 macOS Tahoe 26.1

```
1 000 000 records with BenchmarksDotNet

// * Summary *

BenchmarkDotNet v0.15.6, macOS 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.103
  [Host]    : .NET 10.0.3 (10.0.3, 10.0.326.7603), Arm64 RyuJIT armv8.0-a
  .NET 10.0 : .NET 10.0.3 (10.0.3, 10.0.326.7603), Arm64 RyuJIT armv8.0-a
  .NET 8.0  : .NET 8.0.24 (8.0.24, 8.0.2426.7010), Arm64 RyuJIT armv8.0-a
  .NET 9.0  : .NET 9.0.13 (9.0.13, 9.0.1326.6317), Arm64 RyuJIT armv8.0-a


| Method        | Job       | Runtime   | Mean     | Error    | StdDev   | Allocated |
|-------------- |---------- |---------- |---------:|---------:|---------:|----------:|
| ParseSchedule | .NET 10.0 | .NET 10.0 | 828.6 ms | 10.15 ms |  9.50 ms |  74.79 KB |
| ParseSchedule | .NET 8.0  | .NET 8.0  | 855.5 ms | 16.89 ms | 19.45 ms |  75.26 KB |
| ParseSchedule | .NET 9.0  | .NET 9.0  | 849.5 ms |  5.70 ms |  5.05 ms |  74.74 KB |
```

```
1 000 000 records. Cold start

> ./SSIMParser -i sample.ssim -o SSIM_output.json

192 MB sample.ssim
1.3 GB SSIM_output.json
```