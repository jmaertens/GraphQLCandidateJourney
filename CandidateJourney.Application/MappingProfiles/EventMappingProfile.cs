using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;

namespace CandidateJourney.Application.MappingProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventModel>()
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations))
                .PreserveReferences();
        }
    }
}