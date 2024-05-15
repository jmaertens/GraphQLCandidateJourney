using BenchmarkDotNet.Attributes;
using System.Net.Http;
using System.Threading.Tasks;

public class InterestBenchmarks : ApiBenchmarkBase
{
    private const string RestApiUrl = "https://localhost:65356/api/interests/GetAllInterests";
    private const string GraphQlApiUrl = "https://localhost:65356/graphql";

    [Benchmark]
    public async Task TestRestApi()
    {
        var response = await client.GetAsync(RestApiUrl);
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task TestGraphQlApi()
    {
        var query = new
        {
            query = @"
            {
                allInterests {
                    nodes{
                      id,
                      name
                    }
                  }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
}