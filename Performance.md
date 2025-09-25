
// * Export *
  BenchmarkDotNet.Artifacts\results\Benchmarks.WordFinderBenchmarks-report.csv
  BenchmarkDotNet.Artifacts\results\Benchmarks.WordFinderBenchmarks-report-github.md
  BenchmarkDotNet.Artifacts\results\Benchmarks.WordFinderBenchmarks-report.html

// * Detailed results *
WordFinderBenchmarks.Find_32x32Optimized: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 81.055 ms, StdErr = 0.084 ms (0.10%), N = 12, StdDev = 0.290 ms
Min = 80.480 ms, Q1 = 80.874 ms, Median = 81.027 ms, Q3 = 81.313 ms, Max = 81.474 ms
IQR = 0.439 ms, LowerFence = 80.214 ms, UpperFence = 81.972 ms
ConfidenceInterval = [80.684 ms; 81.426 ms] (CI 99.9%), Margin = 0.371 ms (0.46% of Mean)
Skewness = -0.19, Kurtosis = 2.05, MValue = 2
-------------------- Histogram --------------------
[80.314 ms ; 81.640 ms) | @@@@@@@@@@@@
---------------------------------------------------

WordFinderBenchmarks.Find_64x64Optimized: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 100.879 ms, StdErr = 0.479 ms (0.48%), N = 15, StdDev = 1.856 ms
Min = 97.961 ms, Q1 = 99.425 ms, Median = 101.116 ms, Q3 = 101.833 ms, Max = 104.623 ms
IQR = 2.407 ms, LowerFence = 95.815 ms, UpperFence = 105.443 ms
ConfidenceInterval = [98.895 ms; 102.864 ms] (CI 99.9%), Margin = 1.985 ms (1.97% of Mean)
Skewness = 0.26, Kurtosis = 2.16, MValue = 2
-------------------- Histogram --------------------
[ 97.776 ms ; 100.084 ms) | @@@@@
[100.084 ms ; 105.173 ms) | @@@@@@@@@@
---------------------------------------------------

WordFinderBenchmarks.Find_32x32SimpleFind: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 79.205 ms, StdErr = 0.115 ms (0.15%), N = 13, StdDev = 0.415 ms
Min = 78.590 ms, Q1 = 78.987 ms, Median = 79.151 ms, Q3 = 79.423 ms, Max = 80.022 ms
IQR = 0.437 ms, LowerFence = 78.331 ms, UpperFence = 80.079 ms
ConfidenceInterval = [78.708 ms; 79.702 ms] (CI 99.9%), Margin = 0.497 ms (0.63% of Mean)
Skewness = 0.42, Kurtosis = 2.06, MValue = 2
-------------------- Histogram --------------------
[78.413 ms ; 80.254 ms) | @@@@@@@@@@@@@
---------------------------------------------------

WordFinderBenchmarks.Find_64x64SimpleFind: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 108.432 ms, StdErr = 0.250 ms (0.23%), N = 19, StdDev = 1.090 ms
Min = 105.846 ms, Q1 = 107.880 ms, Median = 108.406 ms, Q3 = 109.284 ms, Max = 110.368 ms
IQR = 1.404 ms, LowerFence = 105.774 ms, UpperFence = 111.391 ms
ConfidenceInterval = [107.452 ms; 109.413 ms] (CI 99.9%), Margin = 0.981 ms (0.90% of Mean)
Skewness = -0.49, Kurtosis = 2.8, MValue = 2
-------------------- Histogram --------------------
[105.310 ms ; 110.904 ms) | @@@@@@@@@@@@@@@@@@@
---------------------------------------------------

WordFinderBenchmarks.Find_32x32SpaceSavingFind: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 64.084 ns, StdErr = 0.321 ns (0.50%), N = 19, StdDev = 1.398 ns
Min = 62.496 ns, Q1 = 62.972 ns, Median = 63.718 ns, Q3 = 65.055 ns, Max = 67.114 ns
IQR = 2.083 ns, LowerFence = 59.848 ns, UpperFence = 68.179 ns
ConfidenceInterval = [62.827 ns; 65.342 ns] (CI 99.9%), Margin = 1.258 ns (1.96% of Mean)
Skewness = 0.74, Kurtosis = 2.12, MValue = 2
-------------------- Histogram --------------------
[61.808 ns ; 64.129 ns) | @@@@@@@@@@@@
[64.129 ns ; 65.673 ns) | @@@@
[65.673 ns ; 67.327 ns) | @@@
---------------------------------------------------

WordFinderBenchmarks.Find_64x64SpaceSavingFind: DefaultJob
Runtime = .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3; GC = Concurrent Workstation
Mean = 61.771 ns, StdErr = 0.197 ns (0.32%), N = 15, StdDev = 0.763 ns
Min = 60.827 ns, Q1 = 61.124 ns, Median = 61.393 ns, Q3 = 62.546 ns, Max = 62.859 ns
IQR = 1.421 ns, LowerFence = 58.992 ns, UpperFence = 64.678 ns
ConfidenceInterval = [60.955 ns; 62.587 ns] (CI 99.9%), Margin = 0.816 ns (1.32% of Mean)
Skewness = 0.31, Kurtosis = 1.15, MValue = 2
-------------------- Histogram --------------------
[60.522 ns ; 63.265 ns) | @@@@@@@@@@@@@@@
---------------------------------------------------

// * Summary *

BenchmarkDotNet v0.15.4, Windows 11 (10.0.22631.5909/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-12700H 2.30GHz, 1 CPU, 20 logical and 14 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3 [AttachedDebugger]
  DefaultJob : .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3


| Method                    | Mean              | Error            | StdDev           | Gen0       | Gen1      | Allocated   |
|-------------------------- |------------------:|-----------------:|-----------------:|-----------:|----------:|------------:|
| Find_32x32Optimized       |  81,055,095.24 ns |   371,217.226 ns |   289,821.993 ns | 13000.0000 | 1000.0000 | 163647491 B |
| Find_64x64Optimized       | 100,879,310.67 ns | 1,984,617.183 ns | 1,856,412.146 ns | 13000.0000 | 3200.0000 | 164374640 B |
| Find_32x32SimpleFind      |  79,205,363.74 ns |   497,078.850 ns |   415,083.353 ns | 13000.0000 | 3000.0000 | 163690146 B |
| Find_64x64SimpleFind      | 108,432,404.21 ns |   980,692.152 ns | 1,090,036.729 ns | 13000.0000 | 3400.0000 | 164473821 B |
| Find_32x32SpaceSavingFind |          64.08 ns |         1.258 ns |         1.398 ns |     0.0324 |         - |       408 B |
| Find_64x64SpaceSavingFind |          61.77 ns |         0.816 ns |         0.763 ns |     0.0324 |         - |       408 B |

// * Warnings *
Environment
  Summary -> Benchmark was executed with attached debugger

// * Hints *
Outliers
  WordFinderBenchmarks.Find_32x32Optimized: Default  -> 3 outliers were removed (82.66 ms..84.70 ms)
  WordFinderBenchmarks.Find_64x64Optimized: Default  -> 1 outlier  was  removed (117.03 ms)
  WordFinderBenchmarks.Find_32x32SimpleFind: Default -> 2 outliers were removed (84.23 ms, 84.53 ms)
  WordFinderBenchmarks.Find_64x64SimpleFind: Default -> 6 outliers were removed (115.62 ms..125.10 ms)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Gen0      : GC Generation 0 collects per 1000 operations
  Gen1      : GC Generation 1 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ns      : 1 Nanosecond (0.000000001 sec)

// * Diagnostic Output - MemoryDiagnoser *


// ***** BenchmarkRunner: End *****
Run time: 00:01:44 (104.68 sec), executed benchmarks: 6

Global total time: 00:01:55 (115.98 sec), executed benchmarks: 6
// * Artifacts cleanup *
Artifacts cleanup is finished
