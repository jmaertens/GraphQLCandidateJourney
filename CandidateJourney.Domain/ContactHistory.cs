namespace CandidateJourney.Domain
{
    public class ContactHistory
    {
        public Guid Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
       
        public ContactHistory() { }
        
    }
}
