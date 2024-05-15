using CandidateJourney.Domain;
using FluentValidation;
using HotChocolate;
using System;

namespace Application.InputTypes
{
    [GraphQLName("UpdateEventInput")]
    [GraphQLDescription("Input type for updating an existing event.")]
    public class UpdateEventInput
    {
        public UpdateEventInput(string name, string description, string organizer, string eventLink, DateTime startDateTime, AudienceCategory targetAudience, DateTime? endDateTime = null)
        {
            Name = name;
            Description = description;
            Organizer = organizer;
            EventLink = eventLink;
            StartDateTime = startDateTime;
            TargetAudience = targetAudience;
            EndDateTime = endDateTime;
        }

        [GraphQLDescription("The name of the event.")]
        public string Name { get; set; }

        [GraphQLDescription("The description of the event.")]
        public string Description { get; set; }

        [GraphQLDescription("The organizer of the event.")]
        public string Organizer { get; set; }

        [GraphQLDescription("The link to the event.")]
        public string EventLink { get; set; }

        [GraphQLDescription("The start date and time of the event.")]
        public DateTime StartDateTime { get; set; }

        [GraphQLDescription("The end date and time of the event (optional).")]
        public DateTime? EndDateTime { get; set; }

        [GraphQLDescription("The target audience of the event.")]
        public AudienceCategory TargetAudience { get; set; }
    }

    public class UpdateEventInputValidator : AbstractValidator<UpdateEventInput>
    {
        public UpdateEventInputValidator()
        {
            RuleFor(@event => @event.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(5).WithMessage("Name must be at least 5 characters long")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

                RuleFor(@event => @event.Description)
                        .NotEmpty().WithMessage("Description is required")
                        .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

                RuleFor(@event => @event.Organizer)
                        .NotEmpty().WithMessage("Organizer is required")
                        .MinimumLength(3).WithMessage("Organizer must be at least 3 characters long")
                        .MaximumLength(100).WithMessage("Organizer must not exceed 100 characters");
            
                RuleFor(@event => @event.StartDateTime)
                        .NotEmpty().WithMessage("StartDateTime is required")
                        .LessThanOrEqualTo(DateTime.Now).WithMessage("StartDateTime cannot be in the future");

                RuleFor(@event => @event.EndDateTime)
                        .GreaterThanOrEqualTo(@event => @event.StartDateTime).WithMessage("EndDateTime must be greater than or equal to StartDateTime")
                        .When(@event => @event.EndDateTime.HasValue);

                RuleFor(@event => @event.TargetAudience)
                        .IsInEnum().WithMessage("TargetAudience must be a valid enum value");
        }
    }
}
