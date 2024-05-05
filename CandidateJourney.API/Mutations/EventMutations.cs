using Application.Abstractions;
using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace API.Mutations
{
    [MutationType]
    public class EventMutations
    {
        public async Task<Event> AddEventAsync([Service] CandidateJourneyDbContext context, [Service] IEventService eventService, CreateEventInput input, CancellationToken cancellationToken) =>
            await eventService.AddEventAsync(context, input, cancellationToken);
    }
}
