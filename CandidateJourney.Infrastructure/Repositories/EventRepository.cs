using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly CandidateJourneyDbContext _context;

        public EventRepository(CandidateJourneyDbContext context)
        {
            _context = context;
        }

        public Task<List<Event>> GetAllUpcoming(int pageNumber)
        {
            var now = DateTime.Now.ToUniversalTime();
            return _context.Events
                .Where(e => e.StartDateTime > now)
                .OrderBy(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<List<Event>> GetAllPrevious(int pageNumber)
        {
            var now = DateTime.Now.ToUniversalTime();
            return _context.Events
                .Where(e => e.StartDateTime < now)
                .OrderByDescending(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<List<Event>> FilterAllPrevious(int pageNumber, string filterstring)
        {
            var now = DateTime.Now.ToUniversalTime();
            return _context.Events
                .Where(e => e.StartDateTime < now)
                .Where(e => e.Name.ToLower().Contains(filterstring)
                || e.Organizer.ToLower().Contains(filterstring))
                .OrderByDescending(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<List<Event>> FilterAllUpcoming(int pageNumber, string filterstring)
        {
            var now = DateTime.Now.ToUniversalTime();
            return _context.Events
                .Where(e => e.StartDateTime > now)
                .Where(e => e.Name.ToLower().Contains(filterstring)
                || e.Organizer.ToLower().Contains(filterstring))
                .OrderByDescending(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<Event> FindById(Guid id) => _context.Events.Include(e => e.Candidates).ThenInclude(x => x.ContactHistories)
            .Include(e => e.Candidates).ThenInclude(c => c.ContactHistories).ThenInclude(ch => ch.CreatedBy).IgnoreQueryFilters()
            .Include(e => e.CreatedBy).IgnoreQueryFilters()
            .Include(e => e.UpdatedBy).IgnoreQueryFilters()
            .SingleOrDefaultAsync(e => e.Id == id);

        public Task Add(Event e)
        {
            _context.Events.Add(e);
            return _context.SaveChangesAsync();
        }

        public Task UpdateEvent(Event eventToEdit)
        {
            return _context.SaveChangesAsync();
        }

        public Task ArchiveEvent(Event eventToArchive)
        {
            _context.Events.Remove(eventToArchive);
            return _context.SaveChangesAsync();
        }

    }
}
