using CandidateJourney.Domain;

namespace CandidateJourney.Application.Contracts.Models
{
    public class ContactHistoryModel
    {
        public Guid Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}