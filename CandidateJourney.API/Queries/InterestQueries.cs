using Application.Abstractions;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace API.Queries
{
    [QueryType]
    public class InterestQueries
    {
        [GraphQLDescription("Retrieve all interest.")]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Interest>> GetAllInterests([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService)
        {
            return await interestService.GetAllInterestsAsync(context);
        }

        [GraphQLDescription("Retrieve a specific interest by its Id.")]
        public async Task<Interest> GetInterestById(
            [Service] CandidateJourneyDbContext context, 
            [Service] IInterestService interestService,
            [GraphQLDescription("Id of the interest to retrieve.")] int interestId, 
            CancellationToken cancellation)
        {
            return await interestService.GetInterestByIdAsync(context, interestId, cancellation);
        }
    }
}
