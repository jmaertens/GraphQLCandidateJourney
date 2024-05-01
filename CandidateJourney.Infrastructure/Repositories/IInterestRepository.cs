using CandidateJourney.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure.Repositories
{
    public interface IInterestRepository
    {
        Task<List<Interest>> GetAll();
        Task<Interest> FindById(int id);
        Task Add(Interest interest);
        Task UpdateInterest(Interest interestToEdit);
        Task Delete(Interest interest);
    }
}
