using Application.Abstractions;
using CandidateJourney.Infrastructure;
using Location = CandidateJourney.Domain.Location;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class LocationQueries
    {
        [GraphQLDescription("Retrieve all locations.")]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Location> GetAllLocations(
            [Service] ILocationService locationService,
            [Service] CandidateJourneyDbContext context)
        {
            return locationService.GetAllLocations(context);
        }

        [GraphQLDescription("Retrieve a specific location by its Id.")]
        public Task<Location> GetLocationById(
            [Service] ILocationService locationService,
            [Service] CandidateJourneyDbContext context,
            [GraphQLDescription("Id of the location to retrieve.")] Guid locationId,
            CancellationToken cancellationToken)
        {
            return locationService.GetLocationByIdAsync(context, locationId, cancellationToken);
        }
    }
}
