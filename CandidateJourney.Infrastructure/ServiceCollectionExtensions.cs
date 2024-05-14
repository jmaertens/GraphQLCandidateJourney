using CandidateJourney.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateJourney.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCandidateJourneyInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IInterestRepository, InterestRepository>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddHttpContextAccessor();
        services.AddDbContext<CandidateJourneyDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<DatabaseSeeder>();

        return services;
    }
}