namespace CandidateJourney.Domain
{
    public class Event
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string? EventLink { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public AudienceCategory TargetAudience { get; set; }
        public List<Candidate> Candidates { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public void AddCandidate(Candidate candidate)
        {
            if (Candidates == null)
            {
                Candidates = new List<Candidate>();
            }
            if(Candidates.Where(x => x.Email == candidate.Email).Any())
            {
                throw new Exception("A candidate with this email already exists!");
            }
            else
            {
                Candidates.Add(candidate);
            }
        }

        private Event() { }

        public Event(string name, string organizer, string location, DateTime startDateTime, DateTime? endDateTime, 
            AudienceCategory targetAudienceType, string? description, string? eventLink)
        {
            Id = Guid.NewGuid();
            Name = name;
            Organizer = organizer;
            Location = location;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TargetAudience = targetAudienceType;
            Description = description;
            EventLink = eventLink;
            IsDeleted = false;
        }
    }
}
