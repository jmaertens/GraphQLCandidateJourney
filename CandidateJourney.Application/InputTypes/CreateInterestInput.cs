using FluentValidation;
using HotChocolate;
using SendGrid.Helpers.Mail;
using System;

namespace Application.InputTypes
{
    [GraphQLName("CreateInterestInput")]
    [GraphQLDescription("Input type for creating a new interest.")]
    public class CreateInterestInput
    {
        public CreateInterestInput(string name)
        {
            Name = name;
        }

        [GraphQLDescription("The name of the interest.")]
        public string Name { get; set; }
    }

    public class CreateInterestInputValidator : AbstractValidator<CreateInterestInput>
    {
        public CreateInterestInputValidator()
        {
            RuleFor(interest => interest.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
