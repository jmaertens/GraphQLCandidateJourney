using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;

namespace Application.Services
{
    public class GQLEventService
    {
        public async Task<Event> AddEventAsync(CandidateJourneyDbContext context, CreateEventInput input, CancellationToken cancellationToken)
        {
            var newEvent = new Event(input.Name, input.Organizer, input.Location, input.StartDateTime, input.EndDateTime,
                input.TargetAudience, input.Description, input.EventLink);

            context.Events.Add(newEvent);
            await context.SaveChangesAsync(cancellationToken);

            return newEvent;
        }
    }
}
