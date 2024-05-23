using BenchmarkDotNet.Attributes;
using CandidateJourney.Infrastructure;
using System.Diagnostics;

public class EventBenchmarks : ApiBenchmarkBase
{
    private const string GraphQlApiUrl = "https://localhost:65356/graphql";
    private const string RestApiUrl = "https://localhost:65356/api/events/GetAllUpcomingEventsWithoutPagination";
    private static readonly HttpClient _client = new HttpClient();
    
    [Benchmark]
    public async Task TestRestApi()
    {
        var response = await _client.GetAsync(RestApiUrl);
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task TestGraphQlApiEventLocations()
    {
        var query = new
        {
            query = @"
            {
                events{
                  id,
                  name,
                  description,
                  locations{
                    id,
                    name,
                    address
                }
              }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
    /*
    [Benchmark]
    public async Task TestGraphQlApiEventCandidates()
    {
        var query = new
        {
            query = @"
            {
                events{
                  id,
                  name,
                  description,
                  candidates{
                    id,
                    firstName,
                    lastName
                }
              }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
    /*
    [Benchmark]
    public async Task TestGraphQlApiEventUser()
    {
        var query = new
        {
            query = @"
            {
                events{
                  id,
                  name,
                  description,
                  candidates{
                    id,
                    firstName,
                    lastName
                    }
                }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }*/
}