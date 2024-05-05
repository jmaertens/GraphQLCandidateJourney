using CandidateJourney.Domain;

namespace Application.InputTypes
{
    public class CreateEventInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string EventLink { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public AudienceCategory TargetAudience { get; set; }
    }
}
