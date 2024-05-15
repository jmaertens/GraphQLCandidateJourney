using CandidateJourney.Application.Services;
using CandidateJourney.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CandidateJourney.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCandidateJourneyApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRestUserService, RestUserService>();
        services.AddScoped<IRestEventService, RestEventService>();
        services.AddScoped<IRestInterestService, RestInterestService>();
        services.AddScoped<IRestLocationService, RestLocationService>();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(ServiceCollectionExtensions)));

        services.AddCandidateJourneyInfrastructure(configuration);
        
        return services;
    }
}