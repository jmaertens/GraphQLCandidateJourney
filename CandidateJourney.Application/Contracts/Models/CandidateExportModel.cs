namespace CandidateJourney.Application.Contracts.Models
{
    public class CandidateExportModel
    {
        public CandidateExportModel(string firstName, string lastName, string email, string candidateType, string graduationType,
            List<string> interests, string? phoneNumber = null, string? specialization = null, DateTime? dateOfGraduation = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CandidateType = candidateType;
            GraduationType = graduationType;
            Interests = interests ?? new List<string>();
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public DateTime? DateOfGraduation { get; set; }
        public string CandidateType { get; set; }
        public string GraduationType { get; set; }
        public List<string> Interests { get; set; }
    }
}
