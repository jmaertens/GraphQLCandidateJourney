using AutoMapper;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.MappingProfiles
{
    public class CandidateExportMappingProfile : Profile
    {
        public CandidateExportMappingProfile()
        {
            CreateMap<Candidate, CandidateExportModel>();
        }
    }
}
