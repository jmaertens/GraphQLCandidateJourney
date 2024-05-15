using CandidateJourney.Domain;
using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateLocationCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(newLocation => newLocation.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(newLocation => newLocation.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}
