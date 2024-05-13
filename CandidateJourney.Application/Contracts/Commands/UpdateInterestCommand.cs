using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class UpdateInterestCommand
    {
        public string Name { get; set; }
    }
    public class UpdateInterestCommandValidator : AbstractValidator<UpdateInterestCommand>
    {
        public UpdateInterestCommandValidator()
        {
            RuleFor(interest => interest.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}