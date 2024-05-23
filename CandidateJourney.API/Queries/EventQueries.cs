using Application.Abstractions;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.API.Queries
{
    [QueryType]
    public class EventQueries
    {
        [GraphQLDescription("Retrieve events within a specific date range.")]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Event> GetEvents(
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            [GraphQLDescription("Start date to filter events.")] DateTime? from,
            [GraphQLDescription("End date to filter events.")] DateTime? to)
        {
            return eventService.GetEventsByDateRange(context, from, to);
        }

        [GraphQLDescription("Retrieve a specific event by its Id.")]
        public Task<Event> GetEventById(
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            [GraphQLDescription("Id of the event to retrieve.")] Guid eventId,
            CancellationToken cancellationToken)
        {
            return eventService.GetEventByIdAsync(context, eventId, cancellationToken);
        }

        [GraphQLDescription("Retrieve a candidate by Id from an event.")]
        public async Task<Candidate> GetCandidateById(
            [Service] IEventService eventService,
            [Service] CandidateJourneyDbContext context,
            [GraphQLDescription("Id of the event.")] Guid eventId,
            [GraphQLDescription("Id of the candidate.")] Guid candidateId,
            CancellationToken cancellationToken)
        {
            return await eventService.GetCandidateByIdAsync(context, eventId, candidateId, cancellationToken);
        }
    }
}
