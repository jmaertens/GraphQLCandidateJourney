using CandidateJourney.Domain;

namespace CandidateJourney.Application.Contracts.Models
{
    public class EventModel
    {
        public EventModel(
            Guid id,
            string name,
            string organizer,
            DateTime startDateTime,
            AudienceCategory targetAudience,
            UserExportModel createdBy,
            DateTime createdOn,
            bool isDeleted,
            string? description = null,
            string? eventLink = null,
            DateTime? endDateTime = null,
            List<CandidateModel>? candidates = null,
            List<LocationModel>? locations = null,
            UserExportModel? updatedBy = null,
            DateTime? updatedOn = null)
        {
            Id = id;
            Name = name;
            Organizer = organizer;
            StartDateTime = startDateTime;
            TargetAudience = targetAudience;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            IsDeleted = isDeleted;
            Description = description;
            EventLink = eventLink;
            EndDateTime = endDateTime;
            Candidates = candidates ?? new List<CandidateModel>();
            Locations = locations ?? new List<LocationModel>();
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Organizer { get; set; }
        public string? EventLink { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public AudienceCategory TargetAudience { get; set; }
        public List<CandidateModel> Candidates { get; set; }
        public List<LocationModel> Locations { get; set; }
        public UserExportModel CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserExportModel? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
