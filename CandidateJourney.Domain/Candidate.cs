namespace CandidateJourney.Domain
{
    public class Candidate
    {
        public Guid Id { get; private init; }
        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string Email { get; private init; }
        public string? PhoneNumber { get; private init; }
        public string? Specialization { get; private init; }
        public DateTime? DateOfGraduation { get; private init; }
        public CandidateIntent CandidateType { get; private init; }
        public AcademicDegree GraduationType { get; private init; }
        public string? PictureName { get; private init; }
        public string? ExtraInfo { get; private init; }

        private Candidate()
        {
        }

        public Candidate(string firstName, string lastName, string email, string? phoneNumber,
            string? specialization, DateTime? dateOfGraduation, CandidateIntent candidateType, AcademicDegree graduationType, string? extraInfo)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
            CandidateType = candidateType;
            GraduationType = graduationType;
            ExtraInfo = extraInfo;
        }
    }
}
