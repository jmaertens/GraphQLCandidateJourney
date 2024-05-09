using Application.Abstractions;
using Application.InputTypes;
using Application.InputTypes.Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Application.Services
{
    public class EventService : IEventService
    {
        public IQueryable<Event> GetEventsByDateRange(CandidateJourneyDbContext context, DateTime? from, DateTime? to)
        {
            var query = context.Events.AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(e => e.StartDateTime >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(e => e.StartDateTime <= to.Value);
            }

            return query.Where(e => !e.IsDeleted);
        }

        public async Task<Event> GetEventByIdAsync(CandidateJourneyDbContext context, Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await context.Events.Include(e => e.Candidates).FirstOrDefaultAsync(e => e.Id == eventId && !e.IsDeleted, cancellationToken);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            return @event;
        }

        public async Task<Event> AddEventAsync(CandidateJourneyDbContext context, CreateEventInput input, CancellationToken cancellationToken)
        {
            var newEvent = new Event(input.Name, input.Organizer, input.Location, input.StartDateTime, input.EndDateTime,
                input.TargetAudience, input.Description, input.EventLink);

            context.Events.Add(newEvent);
            await context.SaveChangesAsync(cancellationToken);

            return newEvent;
        }

        public async Task<Candidate> AddCandidateToEventAsync(CandidateJourneyDbContext context, Guid eventId, CreateCandidateInput input, CancellationToken cancellationToken)
        {
            var @event = await context.Events.Include(e => e.Candidates).FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            var candidate = new Candidate(input.FirstName, input.LastName, input.Email, input.PhoneNumber,
                                              input.Specialization, input.DateOfGraduation, input.CandidateType,
                                              input.GraduationType, input.PictureName, input.Interests, input.ExtraInfo);

            @event.AddCandidate(candidate);
            await context.SaveChangesAsync(cancellationToken);
            return candidate;
        }

        public async Task<Event> UpdateEventAsync(CandidateJourneyDbContext context, Guid eventId, UpdateEventInput input, CancellationToken cancellationToken)
        {
            var @event = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            @event.Name = input.Name;
            @event.Description = input.Description;
            @event.Organizer = input.Organizer;
            @event.Location = input.Location;
            @event.EventLink = input.EventLink;
            @event.StartDateTime = input.StartDateTime;
            @event.EndDateTime = input.EndDateTime;
            @event.TargetAudience = input.TargetAudience;

            await context.SaveChangesAsync(cancellationToken);
            return @event;
        }

        public async Task<Event> ArchiveEventAsync(CandidateJourneyDbContext context, Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            @event.IsDeleted = true;
            await context.SaveChangesAsync(cancellationToken);
            return @event;
        }
    }
}
