using System;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

public class ApiBenchmark
{
    private static readonly HttpClient client = new HttpClient();

    private const string RestApiUrl = "https://localhost:65356/index.html/GetAllUpcomingEvents?pageNumber=1";
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
                events(from: ""2023-01-01T00:00:00Z"", to: ""2029-12-31T23:59:59Z"") {
                    edges {
                        node {
                            id
                            name
                        }
                    }
                }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
}
