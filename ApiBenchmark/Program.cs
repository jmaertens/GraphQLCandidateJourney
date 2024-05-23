using BenchmarkDotNet.Running;

var config = new BenchmarkConfig();
var summaries = new[]
{
    BenchmarkRunner.Run<EventBenchmarks>(config)
    //BenchmarkRunner.Run<UserBenchmarks>(),
};
Console.ReadLine();
