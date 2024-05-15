using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly CandidateJourneyDbContext _context;

        public LocationRepository(CandidateJourneyDbContext context)
        {
            _context = context;
        }

        public Task<List<Location>> GetAll(int pageNumber)
        {
            return _context.Locations
                .OrderBy(l => l.Name)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<List<Location>> FilterAll(int pageNumber, string filterString)
        {
            return _context.Locations
                .Where(l => l.Name.ToLower().Contains(filterString.ToLower()) ||
                            l.Address.ToLower().Contains(filterString.ToLower()))
                .OrderBy(l => l.Name)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();
        }

        public Task<Location> FindById(Guid id)
        {
            return _context.Locations
                .Include(l => l.Events)
                .ThenInclude(e => e.Candidates)
                .SingleOrDefaultAsync(l => l.Id == id);
        }

        public Task Add(Location location)
        {
            _context.Locations.Add(location);
            return _context.SaveChangesAsync();
        }

        public Task Update(Location locationToEdit)
        {
            _context.Locations.Update(locationToEdit);
            return _context.SaveChangesAsync();
        }

        public Task Delete(Location locationToDelete)
        {
            _context.Locations.Remove(locationToDelete);
            return _context.SaveChangesAsync();
        }
    }
}
