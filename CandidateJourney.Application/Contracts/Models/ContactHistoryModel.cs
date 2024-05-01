using CandidateJourney.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Contracts.Models
{
    public class ContactHistoryModel
    {
        public Guid Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
