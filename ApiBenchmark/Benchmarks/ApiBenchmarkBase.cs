using System.Net.Http;
using BenchmarkDotNet.Attributes;

public abstract class ApiBenchmarkBase
{
    protected static readonly HttpClient client = new HttpClient();

    [GlobalSetup]
    public void Setup()
    {
        
    }
}
