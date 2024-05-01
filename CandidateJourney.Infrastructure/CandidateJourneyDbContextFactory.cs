using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CandidateJourney.Infrastructure;

public class CandidateJourneyDbContextFactory : IDesignTimeDbContextFactory<CandidateJourneyDbContext>
{
    public CandidateJourneyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CandidateJourneyDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost;Database=ordina-candidate-journey;Trusted_Connection=True;");

        return new CandidateJourneyDbContext(optionsBuilder.Options, null!);
    }
}