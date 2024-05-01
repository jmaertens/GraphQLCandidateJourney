using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Services
{
    public interface IInterestService
    {
        Task<List<string>> GetAllInterestNamesAsync();
        Task<List<InterestModel>> GetAllInterestsAsync();
        Task<InterestModel> GetInterestByIdAsync(int id);
        Task<InterestModel> AddInterestAsync(CreateInterestCommand command);
        Task<InterestModel> UpdateInterestAsync(int id, UpdateInterestCommand command);
        Task<InterestModel> DeleteInterestAsync(int id);
    }
}
