using CandidateJourney.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Contracts.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string? EventLink { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public TargetAudienceType TargetAudience { get; set; }
        public List<CandidateModel> Candidates { get; set; }
        public UserExportModel CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserExportModel? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
