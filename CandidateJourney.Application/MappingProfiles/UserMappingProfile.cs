using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;

namespace CandidateJourney.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserModel>();
    }
}