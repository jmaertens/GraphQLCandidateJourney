using AutoMapper;
using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;
using System.Text;

namespace CandidateJourney.Application.Services
{
    public class RestEventService : IRestEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public RestEventService(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<EventModel> AddCandidateToEventAsync(Guid eventId, CreateCandidateCommand command)
        {
            var @event = await _eventRepository.FindById(eventId);
            if (@event == null) throw new Exception("Event not found.");
            string? pictureName = null;
            var newCandidate = new Candidate(command.FirstName!, command.LastName!,
                 command.Email!, command.PhoneNumber, command.Specialization,
                 command.DateOfGraduation, command.CandidateType!, command.AcademicDegree!, pictureName,
                 command.Interests!, command.ExtraInfo);

            @event.AddCandidate(newCandidate);

            await _eventRepository.UpdateEvent(@event);
            return _mapper.Map<EventModel>(@event);
        }

        public async Task<EventModel> AddEventAsync(CreateEventCommand command)
        {
            var newEvent = new Event(command.Name!, command.Organizer!, command.Location!, command.StartDateTime, command.EndDateTime,
                command.TargetAudience, command.Description, command.EventLink);
            await _eventRepository.Add(newEvent);
            return _mapper.Map<EventModel>(newEvent);
        }

        public async Task<List<EventModel>> GetAllUpcomingEventsAsync(int pageNumber, string? filterString)
        {
            List<Event> events;
            if (String.IsNullOrEmpty(filterString))
            {
                events = await _eventRepository.GetAllUpcoming(pageNumber);
            }
            else
            {
                events = await _eventRepository.FilterAllUpcoming(pageNumber, filterString);
            }
            SetEventDatesToLocalTime(events);
            return _mapper.Map<List<EventModel>>(events);
        }

        public async Task<List<EventModel>> GetAllPreviousEventsAsync(int pageNumber, string? filterString)
        {
            List<Event> events;
            if (String.IsNullOrEmpty(filterString))
            {
                events = await _eventRepository.GetAllPrevious(pageNumber);
            }
            else
            {
                events = await _eventRepository.FilterAllPrevious(pageNumber, filterString);
            }
            SetEventDatesToLocalTime(events);
            return _mapper.Map<List<EventModel>>(events);
        }

        public async Task<EventModel> GetEventByIdAsync(Guid id)
        {
            var foundEvent = await _eventRepository.FindById(id);
            foundEvent.StartDateTime = foundEvent.StartDateTime.ToLocalTime();
            foundEvent.EndDateTime = foundEvent.EndDateTime?.ToLocalTime();
            foundEvent.CreatedOn = foundEvent.CreatedOn.ToLocalTime();
            foundEvent.UpdatedOn = foundEvent.UpdatedOn?.ToLocalTime();
            return _mapper.Map<EventModel>(foundEvent);
        }

        public async Task<EventModel> ArchiveEventAsync(Guid eventId)
        {
            var @event = await _eventRepository.FindById(eventId);
            await _eventRepository.ArchiveEvent(@event);
            return _mapper.Map<EventModel>(@event);
        }

        public async Task<EventModel> UpdateEventAsync(Guid eventId, UpdateEventCommand command)
        {
            var @event = await _eventRepository.FindById(eventId);
            if (@event == null) throw new Exception("Event not found.");
            @event.Name = command.Name;
            @event.Description = command.Description;
            @event.Organizer = command.Organizer;
            @event.Location = command.Location;
            @event.EventLink = command.EventLink;
            @event.StartDateTime = command.StartDateTime;
            @event.EndDateTime = command.EndDateTime;
            @event.TargetAudience = command.TargetAudience;

            await _eventRepository.UpdateEvent(@event);
            return _mapper.Map<EventModel>(@event);
        }

        public async Task<string> GetCandidateExportByEventIdAsync(Guid id)
        {
            var @event = await _eventRepository.FindById(id);
            var candidates = @event.Candidates;

            var candidatesExportModels = _mapper.Map<List<CandidateExportModel>>(candidates);

            var builder = new StringBuilder();
            builder.AppendLine("First name; Last name; Email; Phone number; Specialization; " +
                    "Date of graduation; Type of graduation; Is looking for; Interests; Extra info");
            foreach (var can in candidates)
            {
                var interestBuilder = new StringBuilder();
                if (can.Interests != null)
                {
                    can.Interests.ForEach(e =>
                    {
                        interestBuilder.Append($"{e.ToString()},");
                    });
                    interestBuilder.Remove(interestBuilder.Length - 1, 1);
                }
                builder.AppendLine($"{can.FirstName};{can.LastName};{can.Email};{can.PhoneNumber};" +
                    $"{can.Specialization};{can.DateOfGraduation.Value.ToString("M/yyyy")};{can.GraduationType};{can.CandidateType};{interestBuilder}; {can.ExtraInfo}");
            }
            return builder.ToString();
        }

        public async Task<CandidateModel> GetCandidateByIdAsync(Guid eventId, Guid candidateId)
        {
            Event @event = await _eventRepository.FindById(eventId);
            Candidate candidate = @event.Candidates.SingleOrDefault(c => c.Id == candidateId);
            return _mapper.Map<CandidateModel>(candidate);
        }

        public async Task<EventModel> RemoveCandidateByIdAsync(Guid eventId, Guid candidateId)
        {
            Event @event = await _eventRepository.FindById(eventId);

            if (@event == null)
            {
                throw new Exception("Event not found.");
            }
            var candidate = @event.Candidates.SingleOrDefault(c => c.Id == candidateId);
            if (candidate != null)
            {
                @event.Candidates.Remove(candidate);
            }
            else
            {
                throw new Exception("Candidate not found.");
            }

            await _eventRepository.UpdateEvent(@event);
            return _mapper.Map<EventModel>(@event);
        }

        public async Task<EventModel> AddContactHistoryToCandidateInEvent(CreateContactHistoryCommand command)
        {
            Event @event = await _eventRepository.FindById(command.EventId);
            if (@event == null) throw new Exception("Event not found");
            foreach (var id in command.CandidateId)
            {
                var contactHistory = new ContactHistory();
                var candidate = @event.Candidates.SingleOrDefault(candidate => candidate.Id == id);
                if (candidate == null) throw new Exception("Candidate not found");
                candidate.AddContactHistory(contactHistory);
            }
            await _eventRepository.UpdateEvent(@event);
            return _mapper.Map<EventModel>(@event);
        }

        private List<Event> SetEventDatesToLocalTime(List<Event> events)
        {
            events.ForEach(e => e.StartDateTime = e.StartDateTime.ToLocalTime());
            events.ForEach(e => e.EndDateTime = e.EndDateTime?.ToLocalTime());
            return events;
        }
    }
}