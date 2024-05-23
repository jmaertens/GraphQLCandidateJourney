using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(e => e.Locations)
                .Include(e => e.Candidates)
                .Where(e => e.StartDateTime > now)
                .OrderBy(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public async Task<List<Event>> GetAllUpcomingWithoutPagination()
        {
            var now = DateTime.Now.ToUniversalTime();
            var events = await _context.Events.Include(e => e.Locations).Include(e => e.Candidates).ToListAsync();
            return events;
        }

        public Task<List<Event>> GetAllPrevious(int pageNumber)
        {
            var now = DateTime.Now.ToUniversalTime();
            return _context.Events
                .Include(e => e.Locations)
                .Include(e => e.Candidates)
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
                .Include(e => e.Locations)
                .Include(e => e.Candidates)
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
                .Include(e => e.Locations)
                .Include(e => e.Candidates)
                .Where(e => e.StartDateTime > now)
                .Where(e => e.Name.ToLower().Contains(filterstring)
                || e.Organizer.ToLower().Contains(filterstring))
                .OrderByDescending(e => e.StartDateTime)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<Event> FindById(Guid id) 
        {
            var @event = _context.Events
                .Include(e => e.Candidates)
                .Include(e => e.CreatedBy)
                .Include(e => e.UpdatedBy)
                .Include(e => e.Locations)
                .SingleOrDefaultAsync(e => e.Id == id);
            
            if (@event == null) throw new Exception("No events found.");

            return @event;
        }
              
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
