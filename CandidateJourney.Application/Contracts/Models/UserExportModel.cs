namespace CandidateJourney.Application.Contracts.Models
{
    public class UserExportModel
    {
        public UserExportModel(string firstName, string lastName, bool isDeleted)
        {
            FirstName = firstName;
            LastName = lastName;
            IsDeleted = isDeleted;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
