using Application.Abstractions;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Queries
{
    [QueryType]
    public class InterestQueries
    {
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Interest>> GetAllInterests([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService)
        {
            return await interestService.GetAllInterestsAsync(context);
        }

        [GraphQLDescription("Retrieve a specific interest by its Id.")]
        public async Task<Interest> GetInterestById([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService, int id)
        {
            return await interestService.GetInterestByIdAsync(context, id);
        }
    }
}
