using CandidateJourney.Domain;

namespace CandidateJourney.Infrastructure.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllUpcoming(int pageNumber);
        Task<List<Event>> GetAllPrevious(int pageNumber);
        Task<List<Event>> FilterAllUpcoming(int pageNumber, string filterstring);
        Task<List<Event>> FilterAllPrevious(int pageNumber, string filterstring);
        Task<Event> FindById(Guid id);
        Task Add(Event e);
        Task UpdateEvent(Event eventToEdit);
        Task ArchiveEvent(Event eventToArchive);
    }
}
