using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;

namespace CandidateJourney.Application.Services
{
    public interface IRestLocationService
    {
        Task<List<LocationModel>> GetAllLocationsAsync(int pageNumber, string? filterString);
        Task<LocationModel> GetLocationByIdAsync(Guid id);
        Task<LocationModel> AddLocationAsync(CreateLocationCommand command);
        Task<LocationModel> UpdateLocationAsync(Guid locationId, UpdateLocationCommand command);
        Task<LocationModel> DeleteLocationAsync(Guid locationId);
    }
}
