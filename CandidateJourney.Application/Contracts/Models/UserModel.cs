namespace CandidateJourney.Application.Contracts.Models;

public class UserModel
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public bool IsRegistered { get; set; }
}