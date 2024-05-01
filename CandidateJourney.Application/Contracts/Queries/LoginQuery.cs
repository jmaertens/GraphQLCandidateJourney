using FluentValidation;

namespace CandidateJourney.Application.Contracts.Queries;

public class LoginQuery
{
    public string? EmailAddress { get; set; }

    public string? Password { get; set; }
}

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        
        RuleFor(x => x.EmailAddress).EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
            .WithMessage("Invalid email address");
    }
}