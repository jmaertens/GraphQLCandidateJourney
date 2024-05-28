using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using System.IO;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(Job.Default.WithWarmupCount(5).WithIterationCount(50));
        AddLogger(ConsoleLogger.Default);

        string logDirectory = @"C:\Users\Jonas\Desktop\⠀\Bachelorproef\INA\GraphQl\GraphQLCandidateJourney\ApiBenchmark\Output";
        AddLogger(new FileLogger(logDirectory));

        AddExporter(HtmlExporter.Default);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddColumnProvider(DefaultColumnProviders.Instance);
    }
}
