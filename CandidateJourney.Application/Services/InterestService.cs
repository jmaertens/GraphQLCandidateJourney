using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions;
using CandidateJourney.Application.Contracts.Commands;
using Application.InputTypes;
using HotChocolate;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class InterestService : IInterestService
    {

        public async Task<IQueryable<Interest>> GetAllInterestsAsync(CandidateJourneyDbContext context)
        {
            var interests = context.Interests.AsQueryable();
            if (interests == null)
                throw new GraphQLException(new Error($"No interest were found.", "INTERESTS_NOT_FOUND"));

            return interests;
        }

        public async Task<Interest> GetInterestByIdAsync(CandidateJourneyDbContext context, int interestId)
        {
            var interest = await context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (interest == null)
                throw new GraphQLException(new Error($"Interest with Id {interestId} not found.", "INTEREST_NOT_FOUND"));

            return interest;
        }

        public async Task<Interest> AddInterestAsync(CandidateJourneyDbContext context, CreateInterestInput input)
        {
            var newInterest = new Interest()
            {
                Name = input.Name
            };

            context.Interests.Add(newInterest);
            await context.SaveChangesAsync();
            return newInterest;
        }

        public async Task<Interest> UpdateInterestAsync(CandidateJourneyDbContext context, int interestId, UpdateInterestInput input)
        {
            var interest = await context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (interest == null)
                throw new GraphQLException(new Error($"Interest with Id {interestId} not found.", "INTEREST_NOT_FOUND"));

            interest.Name = input.Name;
            
            await context.SaveChangesAsync();
            return interest;
        }

        public async Task<Interest> DeleteInterestAsync(CandidateJourneyDbContext context, int interestId)
        {
            var interest = await context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (interest == null)
                throw new GraphQLException(new Error($"Interest with Id {interestId} not found.", "INTEREST_NOT_FOUND"));
            
            context.Interests.Remove(interest);
            await context.SaveChangesAsync();
            return interest;
        }
    }
}
