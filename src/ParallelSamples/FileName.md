// * Summary *

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26100.6899/24H2/2024Update/HudsonValley)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 10.0.100
  [Host]   : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4

Job=.NET 8.0  Runtime=.NET 8.0

| Method   | N | M        | Mean     | Error   | StdDev  | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |-- |--------- |---------:|--------:|--------:|------:|--------:|----------:|------------:|
| Thread   | 2 | 10000000 | 240.8 ms | 4.55 ms | 8.43 ms |  1.00 |    0.05 |   3.86 KB |        1.00 |
| Task     | 2 | 10000000 | 221.3 ms | 4.27 ms | 5.99 ms |  0.92 |    0.04 |   1.92 KB |        0.50 |
| Parallel | 2 | 10000000 | 185.0 ms | 1.81 ms | 1.60 ms |  0.77 |    0.03 |   5.91 KB |        1.53 |
| PLINQ    | 2 | 10000000 | 231.0 ms | 4.59 ms | 5.80 ms |  0.96 |    0.04 |  11.79 KB |        3.05 |
