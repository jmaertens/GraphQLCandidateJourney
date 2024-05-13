using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;

namespace CandidateJourney.Application.Services
{
    public interface IRestEventService
    {
        Task<List<EventModel>> GetAllUpcomingEventsAsync(int pageNumber, string? filterString);
        Task<List<EventModel>> GetAllPreviousEventsAsync(int pageNumber, string? filterString);
        Task<EventModel> GetEventByIdAsync(Guid id);
        Task<EventModel> AddEventAsync(CreateEventCommand command);
        Task<EventModel> AddCandidateToEventAsync(Guid eventId, CreateCandidateCommand command);
        Task<string> GetCandidateExportByEventIdAsync(Guid id);
        Task<EventModel> UpdateEventAsync(Guid eventId, UpdateEventCommand command);
        Task<CandidateModel> GetCandidateByIdAsync(Guid eventId, Guid candidateId);
        Task<EventModel> ArchiveEventAsync(Guid eventId);
        Task<EventModel> AddContactHistoryToCandidateInEvent(CreateContactHistoryCommand command);
        Task<EventModel> RemoveCandidateByIdAsync(Guid eventId, Guid candidateId);
    }
}