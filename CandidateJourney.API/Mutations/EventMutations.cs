using Application.Abstractions;
using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace API.Mutations
{
    [MutationType]
    public class EventMutations
    {
        [GraphQLDescription("Add a new event.")]
        public async Task<Event> AddEventAsync([Service] CandidateJourneyDbContext context, [Service] IEventService eventService, CreateEventInput input, CancellationToken cancellationToken) =>
            await eventService.AddEventAsync(context, input, cancellationToken);

        [GraphQLDescription("Add a new candidate to an event by its Id.")]
        public async Task<Candidate> AddCandidateToEventAsync(
            [GraphQLDescription("Id of the event where a candidate should be added to.")] Guid eventId,
            CreateCandidateInput input,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.AddCandidateToEventAsync(context, eventId, input, cancellationToken);
        }

        [GraphQLDescription("Update an event by its Id.")]
        public async Task<Event> UpdateEventAsync(
            [GraphQLDescription("Id of the event to update.")] Guid eventId,
            UpdateEventInput input,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.UpdateEventAsync(context, eventId, input, cancellationToken);
        }

        [GraphQLDescription("Archive an event by its Id.")]
        public async Task<Event> ArchiveEventAsync(
            [GraphQLDescription("Id of the event to archive.")] Guid eventId,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.ArchiveEventAsync(context, eventId, cancellationToken);
        }

        [GraphQLDescription("Delete an event by its Id.")]
        public async Task<Event> DeleteCandidateFromEventAsync(
            [GraphQLDescription("Id of the event.")] Guid eventId,
            [GraphQLDescription("Id of the candidate to delete from event.")] Guid candidateId,
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            CancellationToken cancellationToken)
        {
            return await eventService.DeleteCandidateByIdAsync(context, eventId, candidateId, cancellationToken);
        }

        [GraphQLDescription("Add a location to an event.")]
        public async Task<Event> AddLocationToEventAsync(
            [Service] CandidateJourneyDbContext context,
            [Service] IEventService eventService,
            AddLocationToEventInput input,
            CancellationToken cancellationToken)
        {
            return await eventService.AddLocationToEventAsync(context, input, cancellationToken);
        }

        [GraphQLDescription("Registers a candidate to an event.")]
        public async Task<Event> AddCandidateToEventAsync(
            [Service] CandidateJourneyDbContext context,
            [Service] IEventService eventService,
            [GraphQLDescription("Id of the event.")] Guid eventId,
            AddCandidateToEventInput input,
            CancellationToken cancellationToken)
        {
            return await eventService.AddCandidateToEventAsync(context, input, eventId, cancellationToken);
        }

    }
}
