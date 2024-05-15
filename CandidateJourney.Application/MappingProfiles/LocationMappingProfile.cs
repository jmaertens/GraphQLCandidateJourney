using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;

namespace CandidateJourney.Application.MappingProfiles
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationModel>();
        }
    }
}