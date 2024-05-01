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
        public CandidateType CandidateType { get; private init; }
        public GraduationType GraduationType { get; private init; }
        public string? PictureName { get; private init; }
        public List<string> Interests { get; private init; }
        public List<ContactHistory> ContactHistories { get; set; }
        public string? ExtraInfo { get; private init; }

        private Candidate()
        {
            ContactHistories = new List<ContactHistory>();
        }

        public Candidate(string firstName, string lastName, string email, string? phoneNumber,
            string? specialization, DateTime? dateOfGraduation, CandidateType candidateType, GraduationType graduationType,
            string? pictureName, List<string> interests, string? extraInfo)
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
            PictureName = pictureName;
            Interests = interests;
            ExtraInfo = extraInfo;
        }

        public void AddContactHistory(ContactHistory contactHistory)
        {
            ContactHistories.Add(contactHistory);
        }
    }
}
