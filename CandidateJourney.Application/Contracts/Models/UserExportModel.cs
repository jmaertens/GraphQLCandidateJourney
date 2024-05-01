using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Contracts.Models
{
    public class UserExportModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
