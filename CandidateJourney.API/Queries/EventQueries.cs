using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class EventQueries
    {
        public async Task<IEnumerable<Event>> GetAllEvents([Service] CandidateJourneyDbContext context, CancellationToken cancellationToken)
        {
            return await context.Events.ToListAsync(cancellationToken);
        }
    }
}
