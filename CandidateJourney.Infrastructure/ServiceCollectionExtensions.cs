using CandidateJourney.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateJourney.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCandidateJourneyInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddHttpContextAccessor();
        services.AddDbContext<CandidateJourneyDbContext>(options => options.UseSqlServer(connectionString));
        
        return services;
    }
}