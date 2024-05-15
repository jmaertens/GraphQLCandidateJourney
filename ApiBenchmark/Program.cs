using BenchmarkDotNet.Running;

var summaries = new[]
{
    //BenchmarkRunner.Run<EventBenchmarks>(),
    //BenchmarkRunner.Run<UserBenchmarks>(),
    BenchmarkRunner.Run<InterestBenchmarks>()
};
Console.ReadLine();
