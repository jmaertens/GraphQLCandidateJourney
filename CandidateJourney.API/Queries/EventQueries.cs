using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.API.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class EventQueries : Query
    {
        public async Task<IEnumerable<Event>> GetEvents([Service] CandidateJourneyDbContext dbContext)
        {
            return await dbContext.Events.ToListAsync();
        }
    }
}
