using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.MappingProfiles
{
    public class ContactHistoryMappingProfile : Profile
    {
        public ContactHistoryMappingProfile()
        {
            CreateMap<ContactHistory, ContactHistoryModel>();
        }
    }
}