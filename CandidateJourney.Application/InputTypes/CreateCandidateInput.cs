using CandidateJourney.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InputTypes
{
    public class CreateCandidateInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public DateTime? DateOfGraduation { get; set; }
        public CandidateIntent CandidateType { get; set; }
        public AcademicDegree GraduationType { get; set; }
        public string? PictureName { get; set; }
        public List<string> Interests { get; set; }
        public string? ExtraInfo { get; set; }
    }
}
