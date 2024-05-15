using Application.Abstractions;
using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        public IQueryable<Location> GetAllLocations(CandidateJourneyDbContext context)
        {
            return context.Locations;
        }

        public async Task<Location> GetLocationByIdAsync(CandidateJourneyDbContext context, Guid locationId, CancellationToken cancellationToken)
        {
            return await context.Locations.FindAsync(new object[] { locationId }, cancellationToken);
        }

        public async Task<Location> AddLocationAsync(CandidateJourneyDbContext context, CreateLocationInput input, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Address = input.Address
            };

            context.Locations.Add(location);
            await context.SaveChangesAsync(cancellationToken);

            return location;
        }

        public async Task<Location> UpdateLocationAsync(CandidateJourneyDbContext context, Guid locationId, UpdateLocationInput input, CancellationToken cancellationToken)
        {
            var location = await context.Locations.FindAsync(new object[] { locationId }, cancellationToken);

            if (location == null)
            {
                throw new Exception("Location not found");
            }

            location.Name = input.Name;
            location.Address = input.Address;

            await context.SaveChangesAsync(cancellationToken);

            return location;
        }

        public async Task<Location> DeleteLocationAsync(CandidateJourneyDbContext context, Guid locationId, CancellationToken cancellationToken)
        {
            var location = await context.Locations.FindAsync(new object[] { locationId }, cancellationToken);

            if (location == null)
            {
                throw new Exception("Location not found");
            }

            context.Locations.Remove(location);
            await context.SaveChangesAsync(cancellationToken);

            return location;
        }
    }
}
