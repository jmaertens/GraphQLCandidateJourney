namespace CandidateJourney.Application.Contracts.Models
{
    public class CandidateExportModel
    {
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