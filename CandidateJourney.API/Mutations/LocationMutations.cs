using Application.Abstractions;
using Application.InputTypes;
using CandidateJourney.Infrastructure;
using Location = CandidateJourney.Domain.Location;

namespace API.Mutations
{
    [MutationType]
    public class LocationMutations
    {
        [GraphQLDescription("Add a new location.")]
        public async Task<Location> AddLocationAsync(
            [Service] CandidateJourneyDbContext context,
            [Service] ILocationService locationService,
            CreateLocationInput input,
            CancellationToken cancellationToken)
        {
            return await locationService.AddLocationAsync(context, input, cancellationToken);
        }

        [GraphQLDescription("Update a location by its Id.")]
        public async Task<Location> UpdateLocationAsync(
            [GraphQLDescription("Id of the location to update.")] Guid locationId,
            UpdateLocationInput input,
            [Service] ILocationService locationService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await locationService.UpdateLocationAsync(context, locationId, input, cancellationToken);
        }

        [GraphQLDescription("Delete a location by its Id.")]
        public async Task<Location> DeleteLocationAsync(
            [GraphQLDescription("Id of the location to delete.")] Guid locationId,
            [Service] ILocationService locationService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await locationService.DeleteLocationAsync(context, locationId, cancellationToken);
        }
    }
}
