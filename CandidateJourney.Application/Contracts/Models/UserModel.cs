namespace CandidateJourney.Application.Contracts.Models
{
    public class UserModel
    {
        public UserModel(Guid id, string firstName, string lastName, string emailAddress, bool isRegistered)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            IsRegistered = isRegistered;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsRegistered { get; set; }
    }
}
