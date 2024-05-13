using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;

namespace CandidateJourney.Application.MappingProfiles
{
    public class UserExportMappingProfile : Profile
    {
        public UserExportMappingProfile()
        {
            CreateMap<User, UserExportModel>();
        }
    }
}