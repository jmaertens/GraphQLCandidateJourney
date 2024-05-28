using BenchmarkDotNet.Attributes;
using CandidateJourney.Infrastructure;
using System.Diagnostics;

public class EventBenchmarks : ApiBenchmarkBase
{
    private const string GraphQlApiUrl = "https://localhost:65356/graphql";
    private const string RestGetAllUpcomingEvents = "https://localhost:65356/api/events/GetAllUpcomingEvents?pageNumber=1";
    private const string RestGetAllUpcomingEventsWithoutPagination = "https://localhost:65356/api/events/GetAllUpcomingEventsWithoutPagination";
    private const string RestEventById = "https://localhost:65356/api/events/";

    private static readonly HttpClient _client = new HttpClient();
    
    
    #region GetAllUpcomingEvents
    [Benchmark]
    public async Task TestRestGetAllUpcomingEvents()
    {
        var response = await _client.GetAsync(RestGetAllUpcomingEvents);
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task TestGraphQlGetAllUpcomingEvents()
    {
        var query = new
        {
            query = @"
            {
                  events(
                    first: 6, where: {
                      startDateTime: { gte: ""2024-05-24T00:00:00"" }
                      }
                    order: { startDateTime: ASC }
                  ) {
                    pageInfo {
                      hasNextPage
                    }
                    nodes {
                      id
                      name
                      description
                      organizer
                      eventLink
                      startDateTime
                      endDateTime
                      targetAudience
                      locations {
                        id
                        name
                        address
                      }
                      candidates {
                        id
                        firstName
                        lastName
                        email
                        phoneNumber
                        specialization
                        dateOfGraduation
                        candidateType
                        graduationType
                      }
                    }
                  }
                
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
    #endregion
    
    /*
    #region GetAllUpcomingEventsWithoutPagination
    [Benchmark]
    public async Task TestRestGetAllUpcomingEventsWithoutPagination()
    {
        var response = await _client.GetAsync(RestGetAllUpcomingEventsWithoutPagination);
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task TestGraphQlGetAllUpcomingEventsWithoutPagination()
    {
        var query = new
        {
            query = @"
            {
                
                  events(
                    where: {
                      startDateTime: { lte: ""2024-05-24T00:00:00"" }
                      }
                    order: { startDateTime: ASC }
                    ) {
                    nodes {
                      id
                      name
                      description
                      organizer
                      eventLink
                      startDateTime
                      endDateTime
                      targetAudience
                      locations {
                        id
                        name
                        address
                      }
                      candidates {
                        id
                        firstName
                        lastName
                        email
                        phoneNumber
                        specialization
                        dateOfGraduation
                        candidateType
                        graduationType
                      }
                    }
                  }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
    #endregion
   */ 
   /*
    #region EventById
    [Benchmark]
    public async Task TestRestEventById()
    {
        var response = await _client.GetAsync(RestEventById + "28EDFD09-D7C6-4349-8E5D-006AD88AB4FC");
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task TestGraphQlGetEventById()
    {
        var query = new
        {
            query = @"
            {
                  eventById(eventId: ""28EDFD09-D7C6-4349-8E5D-006AD88AB4FC""){
                    id
                      name
                      description
                      organizer
                      eventLink
                      startDateTime
                      endDateTime
                      targetAudience
                      locations {
                        id
                        name
                        address
                      }
                      candidates {
                        id
                        firstName
                        lastName
                        email
                        phoneNumber
                        specialization
                        dateOfGraduation
                        candidateType
                        graduationType
                    }
                  }
            }"
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(GraphQlApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
    #endregion
    */
}