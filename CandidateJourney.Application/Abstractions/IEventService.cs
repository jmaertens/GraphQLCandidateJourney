using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Abstractions
{
    public interface IEventService
    {
        IQueryable<Event> GetEventsByDateRange(CandidateJourneyDbContext context, DateTime? from, DateTime? to);
        Task<Event> GetEventByIdAsync(CandidateJourneyDbContext context, Guid eventId, CancellationToken cancellationToken);

        Task<Event> AddEventAsync(CandidateJourneyDbContext context, CreateEventInput input, CancellationToken cancellationToken);
        Task<Event> UpdateEventAsync(CandidateJourneyDbContext context, Guid eventId, UpdateEventInput input, CancellationToken cancellationToken);
        Task<Event> ArchiveEventAsync(CandidateJourneyDbContext context, Guid eventId, CancellationToken cancellationToken);
        Task<Candidate> AddCandidateToEventAsync(CandidateJourneyDbContext context, Guid eventId, CreateCandidateInput input, CancellationToken cancellationToken);
        Task<Candidate> GetCandidateByIdAsync(CandidateJourneyDbContext context, Guid eventId, Guid candidateId, CancellationToken cancellationToken);
        Task<Event> DeleteCandidateByIdAsync(CandidateJourneyDbContext context, Guid eventId, Guid candidateId, CancellationToken cancellationToken);
    }
}
