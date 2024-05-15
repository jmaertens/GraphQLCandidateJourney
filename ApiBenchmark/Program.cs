using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<ApiBenchmark>(new BenchmarkConfig());