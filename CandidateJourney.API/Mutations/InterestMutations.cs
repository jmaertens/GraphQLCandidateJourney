namespace API.Mutations
{
    using System.Threading.Tasks;
    using Application.Abstractions;
    using Application.InputTypes;
    using global::CandidateJourney.Domain;
    using global::CandidateJourney.Infrastructure;
    using HotChocolate;
    using HotChocolate.Types;

    namespace CandidateJourney.API.Mutations
    {
        [MutationType]
        public class InterestMutations
        {
            [GraphQLDescription("Add a new interest.")]
            public async Task<Interest> AddInterestAsync([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService, CreateInterestInput input)
            {
                return await interestService.AddInterestAsync(context, input);
            }

            [GraphQLDescription("Update an interest by its Id.")]
            public async Task<Interest> UpdateInterestAsync(
                [Service] CandidateJourneyDbContext context, 
                [Service] IInterestService interestService,
                [GraphQLDescription("Id of the interest.")] int interestId,
                UpdateInterestInput input)
            {
                return await interestService.UpdateInterestAsync(context, interestId, input);
            }

            [GraphQLDescription("Delete an interest by its Id.")]
            public async Task<Interest> DeleteInterestAsync(
                [Service] CandidateJourneyDbContext context, 
                [Service] IInterestService interestService,
                [GraphQLDescription("Id of the interest.")] int interestId)
            {
                return await interestService.DeleteInterestAsync(context, interestId);
            }
        }
    }
}
