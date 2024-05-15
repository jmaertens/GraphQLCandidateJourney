namespace CandidateJourney.Domain;

public class User
{
    public Guid Id { get; private init; }

    public string FirstName { get; private init; }
    
    public string LastName { get; private init; }
    
    public string EmailAddress { get; private init; }

    public string? PasswordHash { get; private set; }

    // When a user is invited but not yet registered, their password will be null
    public bool IsRegistered => PasswordHash != null;

    public bool IsDeleted { get; set; }

    public User(string firstName, string lastName, string emailAddress)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        IsDeleted = false;
    }

    public User(Guid id, string firstName, string lastName, string emailAddress)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        IsDeleted = false;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
}
