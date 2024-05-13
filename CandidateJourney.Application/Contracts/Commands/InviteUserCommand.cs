using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands;

public class InviteUserCommand
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }
}

public class InviteUserCommandValidator : AbstractValidator<InviteUserCommand>
{
    public InviteUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address is required");

        RuleFor(x => x.EmailAddress).EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
            .WithMessage("Invalid email address");
    }
}