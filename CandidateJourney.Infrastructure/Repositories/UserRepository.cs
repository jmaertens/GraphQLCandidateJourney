using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CandidateJourneyDbContext _context;

    public UserRepository(CandidateJourneyDbContext context)
    {
        _context = context;
    }

    public Task<List<User>> GetAll() => _context.Users.ToListAsync();

    public Task<User?> FindById(Guid id) => _context.Users.SingleOrDefaultAsync(user => user.Id == id);

    public Task<User?> FindByEmailAddress(string emailAddress) => _context.Users.SingleOrDefaultAsync(user => user.EmailAddress == emailAddress);
    
    public Task Add(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
    }
    
    public Task Update(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }

    public Task ArchiveUser(User userToArchive)
    {
        _context.Users.Remove(userToArchive);
        return _context.SaveChangesAsync();
    }
}