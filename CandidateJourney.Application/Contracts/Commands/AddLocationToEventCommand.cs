using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class AddLocationToEventCommand
    {
        public Guid LocationId { get; set; }
    }

    public class AddLocationToEventCommandValidator : AbstractValidator<AddLocationToEventCommand>
    {
        public AddLocationToEventCommandValidator()
        {
            RuleFor(command => command.LocationId).NotEmpty().WithMessage("LocationId is required");
        }
    }
}
