using Application.Abstractions;
using Application.InputTypes;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using FluentValidation;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class EventService : IEventService
    {
        public IQueryable<Event> GetEventsByDateRange(CandidateJourneyDbContext context, DateTime? from, DateTime? to)
        {
            var query = context.Events.Include(e => e.Candidates).AsQueryable();

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
            var @event = await context.Events.Include(e => e.Candidates).Include(e => e.Locations).FirstOrDefaultAsync(e => e.Id == eventId && !e.IsDeleted, cancellationToken);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            return @event;
        }

        public async Task<Event> AddEventAsync(CandidateJourneyDbContext context, CreateEventInput input, CancellationToken cancellationToken)
        {
            var validator = new CreateEventInputValidator();

            var validationResult = await validator.ValidateAsync(input, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage, code: e.PropertyName));
                throw new GraphQLException(errors);
            }

            var newEvent = new Event(input.Name, input.Organizer, input.StartDateTime, input.EndDateTime,
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

            var validator = new CreateCandidateInputValidator();

            var validationResult = await validator.ValidateAsync(input, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage, code: e.PropertyName));
                throw new GraphQLException(errors);
            }

            var candidate = new Candidate(input.FirstName, input.LastName, input.Email, input.PhoneNumber,
                                              input.Specialization, input.DateOfGraduation, input.CandidateType,
                                              input.GraduationType, input.ExtraInfo);

            @event.AddCandidate(candidate);
            await context.SaveChangesAsync(cancellationToken);
            return candidate;
        }

        public async Task<Event> UpdateEventAsync(CandidateJourneyDbContext context, Guid eventId, UpdateEventInput input, CancellationToken cancellationToken)
        {
            var validator = new UpdateEventInputValidator();
            var validationResult = await validator.ValidateAsync(input, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage, code: e.PropertyName));
                throw new GraphQLException(errors);
            }
            
            var @event = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            @event.Name = input.Name;
            @event.Description = input.Description;
            @event.Organizer = input.Organizer;
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

        public async Task<Candidate> GetCandidateByIdAsync(CandidateJourneyDbContext context, Guid eventId, Guid candidateId, CancellationToken cancellationToken)
        {
            var @event = await context.Events.Include(e => e.Candidates).FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);

            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            var candidate = @event.Candidates.FirstOrDefault(c => c.Id == candidateId);

            if (candidate == null)
                throw new GraphQLException(new Error($"Candidate with Id {candidateId} not found in event with Id {eventId}.", "CANDIDATE_NOT_FOUND"));

            return candidate;
        }
        
        public async Task<Event> DeleteCandidateByIdAsync(CandidateJourneyDbContext context, Guid eventId, Guid candidateId, CancellationToken cancellationToken)
        {
            var @event = await context.Events.Include(e => e.Candidates).FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));

            var candidate = @event.Candidates.FirstOrDefault(c => c.Id == candidateId);
            if (candidate == null)
                throw new GraphQLException(new Error($"Candidate with Id {candidateId} not found in event with Id {eventId}.", "CANDIDATE_NOT_FOUND"));

            @event.Candidates.Remove(candidate);
            await context.SaveChangesAsync(cancellationToken);
            return @event;
        }
        
        public async Task<Event> AddLocationToEventAsync(CandidateJourneyDbContext context, AddLocationToEventInput input, CancellationToken cancellationToken)
        {
            var validator = new AddLocationToEventInputValidator();
            var validationResult = await validator.ValidateAsync(input, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage, code: e.PropertyName));
                throw new GraphQLException(errors);
            }

            var @event = await context.Events.Include(e => e.Locations).FirstOrDefaultAsync(e => e.Id == input.EventId, cancellationToken);
            if (@event == null)
            {
                throw new GraphQLException(new Error($"Event with Id {input.EventId} not found.", "EVENT_NOT_FOUND"));
            }

            var location = await context.Locations.FirstOrDefaultAsync(l => l.Id == input.LocationId, cancellationToken);
            if (location == null)
            {
                throw new GraphQLException(new Error($"Location with Id {input.LocationId} not found.", "LOCATION_NOT_FOUND"));
            }

            if (@event.Locations.Any(l => l.Id == input.LocationId))
            {
                throw new GraphQLException(new Error($"Location with Id {input.LocationId} is already added to the event.", "LOCATION_ALREADY_ADDED"));
            }

            @event.Locations.Add(location);
            await context.SaveChangesAsync(cancellationToken);
            return @event;
        }
        
        public async Task<Event> AddCandidateToEventAsync(CandidateJourneyDbContext context, AddCandidateToEventInput input, Guid eventId, CancellationToken cancellationToken)
        {
            var validator = new AddCandidateToEventInputValidator();
            var validationResult = await validator.ValidateAsync(input, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage, code: e.PropertyName));
                throw new GraphQLException(errors);
            }

            var @event = await context.Events.Include(e => e.Candidates).FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);
            if (@event == null)
            {
                throw new GraphQLException(new Error($"Event with Id {eventId} not found.", "EVENT_NOT_FOUND"));
            }

            var newCandidate = new Candidate(
                input.FirstName,
                input.LastName,
                input.Email,
                input.PhoneNumber,
                input.Specialization,
                input.DateOfGraduation,
                input.CandidateType,
                input.AcademicDegree,
                input.ExtraInfo);

            @event.AddCandidate(newCandidate);
            await context.SaveChangesAsync(cancellationToken);

            return @event;
        }
    }
}
