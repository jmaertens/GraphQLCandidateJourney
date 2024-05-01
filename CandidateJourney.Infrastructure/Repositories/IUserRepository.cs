using CandidateJourney.Domain;

namespace CandidateJourney.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    
    Task<User?> FindById(Guid id);
    
    Task<User?> FindByEmailAddress(string emailAddress);
    
    Task Add(User user);

    Task Update(User user);

    Task ArchiveUser(User userToArchive);
}