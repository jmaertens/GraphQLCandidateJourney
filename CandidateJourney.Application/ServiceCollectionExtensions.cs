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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IInterestService, InterestService>();
        services.AddScoped<IBlobService, BlobService>();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(ServiceCollectionExtensions)));


        services.AddScoped(options => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));

        services.AddCandidateJourneyInfrastructure(configuration);


        return services;
    }
}