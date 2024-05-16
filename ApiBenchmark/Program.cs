using BenchmarkDotNet.Running;

var config = new BenchmarkConfig();
var summaries = new[]
{
    BenchmarkRunner.Run<EventBenchmarks>(config)
    //BenchmarkRunner.Run<UserBenchmarks>(),
    //BenchmarkRunner.Run<InterestBenchmarks>(config)
};
Console.ReadLine();
