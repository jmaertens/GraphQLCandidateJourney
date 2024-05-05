using Application.InputTypes;
using Application.Services;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using FluentValidation;

namespace API.Mutations
{
    [MutationType]
    public class EventMutations
    {
        public async Task<Event> AddEventAsync([Service] CandidateJourneyDbContext context, [Service] GQLEventService eventService, CreateEventInput input, CancellationToken cancellationToken) =>
            await eventService.AddEventAsync(context, input, cancellationToken);
    }
}
