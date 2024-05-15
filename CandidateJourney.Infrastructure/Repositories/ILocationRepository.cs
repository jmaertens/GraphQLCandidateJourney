using CandidateJourney.Domain;

namespace CandidateJourney.Infrastructure.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAll(int pageNumber);
        Task<List<Location>> FilterAll(int pageNumber, string filterString);
        Task<Location> FindById(Guid id);
        Task Add(Location location);
        Task Update(Location locationToEdit);
        Task Delete(Location locationToDelete);
    }
}
