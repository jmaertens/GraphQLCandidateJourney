namespace API.Mutations
{
    using System.Threading.Tasks;
    using Application.Abstractions;
    using Application.InputTypes;
    using FluentValidation;
    using global::CandidateJourney.Application.Contracts.Commands;
    using global::CandidateJourney.Domain;
    using global::CandidateJourney.Infrastructure;
    using HotChocolate;
    using HotChocolate.Types;

    namespace CandidateJourney.API.Mutations
    {
        [MutationType]
        public class InterestMutations
        {
            public async Task<Interest> AddInterestAsync([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService, CreateInterestInput input)
            {
                return await interestService.AddInterestAsync(context, input);
            }
            
            public async Task<Interest> UpdateInterestAsync([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService, int interestId, UpdateInterestInput input)
            {
                return await interestService.UpdateInterestAsync(context, interestId, input);
            }
            
            public async Task<Interest> DeleteInterestAsync([Service] CandidateJourneyDbContext context, [Service] IInterestService interestService, int id)
            {
                return await interestService.DeleteInterestAsync(context, id);
            }
        }
    }
}
