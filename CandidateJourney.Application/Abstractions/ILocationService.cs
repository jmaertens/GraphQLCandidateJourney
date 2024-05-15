using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Abstractions
{
    public interface ILocationService
    {
        IQueryable<Location> GetAllLocations(CandidateJourneyDbContext context);
        Task<Location> GetLocationByIdAsync(CandidateJourneyDbContext context, Guid locationId, CancellationToken cancellationToken);

        Task<Location> AddLocationAsync(CandidateJourneyDbContext context, CreateLocationInput input, CancellationToken cancellationToken);
        Task<Location> UpdateLocationAsync(CandidateJourneyDbContext context, Guid locationId, UpdateLocationInput input, CancellationToken cancellationToken);
        Task<Location> DeleteLocationAsync(CandidateJourneyDbContext context, Guid locationId, CancellationToken cancellationToken);
    }
}
