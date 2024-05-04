using System.Reflection;
using Azure.Storage.Blobs;
using CandidateJourney.Application.Services;
using CandidateJourney.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateJourney.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCandidateJourneyApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCandidateJourneyInfrastructure(configuration);
        
        return services;
    }
}