using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class UsersQueries
    {
        [GraphQLDescription("Retrieve all users.")]
        public async Task<IEnumerable<User>> GetUsers([Service] CandidateJourneyDbContext context, CancellationToken cancellationToken)
        {
            return await context.Users.ToListAsync(cancellationToken);
        }

        [GraphQLDescription("Retrieve a specific user by its Id.")]
        public async Task<User?> GetUserById(
            [GraphQLDescription("Id of the event to retrieve.")] Guid userId, 
            [Service] CandidateJourneyDbContext context, 
            CancellationToken cancellationToken)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
