using Application.Abstractions;
using Application.InputTypes;
using Application.InputTypes.Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace API.Mutations
{
    [MutationType]
    public class EventMutations
    {
        public async Task<Event> AddEventAsync([Service] CandidateJourneyDbContext context, [Service] IEventService eventService, CreateEventInput input, CancellationToken cancellationToken) =>
            await eventService.AddEventAsync(context, input, cancellationToken);

        public async Task<Candidate> AddCandidateToEventAsync(
            Guid eventId,
            CreateCandidateInput input,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.AddCandidateToEventAsync(context, eventId, input, cancellationToken);
        }

        public async Task<Event> UpdateEventAsync(
            Guid eventId,
            UpdateEventInput input,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.UpdateEventAsync(context, eventId, input, cancellationToken);
        }

        public async Task<Event> ArchiveEventAsync(
            Guid eventId,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.ArchiveEventAsync(context, eventId, cancellationToken);
        }

        public async Task<Event> DeleteCandidateFromEventAsync(
            Guid eventId,
            Guid candidateId,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.DeleteCandidateByIdAsync(context, eventId, candidateId, cancellationToken);
        }
    }
}
