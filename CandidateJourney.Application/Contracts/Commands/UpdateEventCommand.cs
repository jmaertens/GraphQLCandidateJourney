using CandidateJourney.Domain;
using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class UpdateEventCommand
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Organizer { get; set; }
        public string? Location { get; set; }
        public string? EventLink { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public AudienceCategory TargetAudience { get; set; }
    }

    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(newEvent => newEvent.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(newEvent => newEvent.Location).NotEmpty().WithMessage("Location is required");
            RuleFor(newEvent => newEvent.Organizer).NotEmpty().WithMessage("Organizer is required");
            RuleFor(newEvent => newEvent.TargetAudience).NotEmpty().WithMessage("TargetAudience is required");
            RuleFor(newEvent => newEvent.TargetAudience).IsInEnum();
            RuleFor(newEvent => newEvent).Must(newEvent => newEvent.EndDateTime >= newEvent.StartDateTime || newEvent.EndDateTime == null).WithMessage("EndDateTime must be greater than StartDateTime");
        }
    }
}