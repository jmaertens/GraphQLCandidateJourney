using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class Query
    {
        private readonly CandidateJourneyDbContext _context;

        public Query(CandidateJourneyDbContext context)
        {
            _context = context;
        }
        
        public string Available()
        {
            return "Available";
        }
    }
}
