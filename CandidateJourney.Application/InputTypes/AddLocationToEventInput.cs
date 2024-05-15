using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("AddLocationToEventInput")]
    [GraphQLDescription("Input type for adding a location to an event.")]
    public class AddLocationToEventInput
    {
        [GraphQLDescription("The ID of the event.")]
        public Guid EventId { get; set; }

        [GraphQLDescription("The ID of the location.")]
        public Guid LocationId { get; set; }
    }

    public class AddLocationToEventInputValidator : AbstractValidator<AddLocationToEventInput>
    {
        public AddLocationToEventInputValidator()
        {
            RuleFor(input => input.EventId)
                .NotEmpty().WithMessage("EventId is required.");

            RuleFor(input => input.LocationId)
                .NotEmpty().WithMessage("LocationId is required.");
        }
    }
}
