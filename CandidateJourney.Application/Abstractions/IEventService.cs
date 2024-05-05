using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Abstractions
{
    public interface IEventService
    {
        Task<Event> AddEventAsync(CandidateJourneyDbContext context, CreateEventInput input, CancellationToken cancellationToken);
    }
}
