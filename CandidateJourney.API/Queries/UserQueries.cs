using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using CandidateJourney.Infrastructure;

namespace CandidateJourney.API.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQueries
    {
        public async Task<IEnumerable<User>> GetUsers([Service] CandidateJourneyDbContext dbContext)
        {
            return await dbContext.Users.ToListAsync();
        }
    }
}
