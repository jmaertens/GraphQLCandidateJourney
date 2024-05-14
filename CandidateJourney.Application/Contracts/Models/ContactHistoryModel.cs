using CandidateJourney.Domain;
using System;

namespace CandidateJourney.Application.Contracts.Models
{
    public class ContactHistoryModel
    {
        public ContactHistoryModel(Guid id, User createdBy, DateTime createdOn)
        {
            Id = id;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            CreatedOn = createdOn;
        }

        public Guid Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
