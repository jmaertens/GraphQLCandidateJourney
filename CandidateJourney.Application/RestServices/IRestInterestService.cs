using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;

namespace CandidateJourney.Application.Services
{
    public interface IRestInterestService
    {
        Task<List<string>> GetAllInterestNamesAsync();
        Task<List<InterestModel>> GetAllInterestsAsync();
        Task<InterestModel> GetInterestByIdAsync(int id);
        Task<InterestModel> AddInterestAsync(CreateInterestCommand command);
        Task<InterestModel> UpdateInterestAsync(int id, UpdateInterestCommand command);
        Task<InterestModel> DeleteInterestAsync(int id);
    }
}