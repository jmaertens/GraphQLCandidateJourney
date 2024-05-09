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
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
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
    }
}
