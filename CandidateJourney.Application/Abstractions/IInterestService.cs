using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Abstractions
{
    public interface IInterestService
    {
        Task<IQueryable<Interest>> GetAllInterestsAsync(CandidateJourneyDbContext context);
        Task<Interest> GetInterestByIdAsync(CandidateJourneyDbContext context, int interestId);
        Task<Interest> AddInterestAsync(CandidateJourneyDbContext context, CreateInterestInput input);
        Task<Interest> UpdateInterestAsync(CandidateJourneyDbContext context, int interestId, UpdateInterestInput input);
        Task<Interest> DeleteInterestAsync(CandidateJourneyDbContext context, int interestId);
    }
}
