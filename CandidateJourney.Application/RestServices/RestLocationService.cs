using AutoMapper;
using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;

namespace CandidateJourney.Application.Services
{
    public class RestLocationService : IRestLocationService
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public RestLocationService(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<LocationModel> AddLocationAsync(CreateLocationCommand command)
        {
            var newLocation = new Location
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Address = command.Address
            };

            await _locationRepository.Add(newLocation);
            return _mapper.Map<LocationModel>(newLocation);
        }

        public async Task<List<LocationModel>> GetAllLocationsAsync(int pageNumber)
        {
            var locations = await _locationRepository.GetAll(pageNumber);
            return _mapper.Map<List<LocationModel>>(locations);
        }

        public async Task<LocationModel> GetLocationByIdAsync(Guid id)
        {
            var location = await _locationRepository.FindById(id);
            if (location == null) throw new Exception("Location not found.");
            return _mapper.Map<LocationModel>(location);
        }

        public async Task<LocationModel> UpdateLocationAsync(Guid locationId, UpdateLocationCommand command)
        {
            var location = await _locationRepository.FindById(locationId);
            if (location == null) throw new Exception("Location not found.");

            location.Name = command.Name;
            location.Address = command.Address;

            await _locationRepository.Update(location);
            return _mapper.Map<LocationModel>(location);
        }

        public async Task<LocationModel> DeleteLocationAsync(Guid locationId)
        {
            var location = await _locationRepository.FindById(locationId);
            if (location == null) throw new Exception("Location not found.");

            await _locationRepository.Delete(location);
            return _mapper.Map<LocationModel>(location);
        }
    }
}
