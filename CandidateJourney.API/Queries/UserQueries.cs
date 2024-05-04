using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CandidateJourney.Infrastructure;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class UsersQueries
    {
        public async Task<IEnumerable<User>> GetAllUsers([Service] CandidateJourneyDbContext context, CancellationToken cancellationToken)
        {
            return await context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserById(Guid userId, [Service] CandidateJourneyDbContext context, CancellationToken cancellationToken)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
