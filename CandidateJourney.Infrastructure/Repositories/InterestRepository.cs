using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly CandidateJourneyDbContext _context;

        public InterestRepository(CandidateJourneyDbContext context)
        {
            _context = context;
        }

        public Task<List<Interest>> GetAll()
        {
          return _context.Interests.ToListAsync();
        }

        public Task<Interest> FindById(int id)
        {
            return _context.Interests.FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task Add(Interest interest)
        {
            _context.Interests.Add(interest);
            return _context.SaveChangesAsync();
        }

        public Task UpdateInterest(Interest interestToEdit)
        {
            return _context.SaveChangesAsync();
        }
        
        public Task Delete(Interest interest)
        {
            _context.Interests.Remove(interest);
            return _context.SaveChangesAsync();
        }
    }
}
