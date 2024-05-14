using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("UpdateInterestInput")]
    [GraphQLDescription("Input type for updating an existing interest.")]
    public class UpdateInterestInput
    {
        public UpdateInterestInput(string name)
        {
            Name = name;
        }

        [GraphQLDescription("The name of the interest.")]
        public string Name { get; set; }
    }

    public class UpdateInterestInputValidator : AbstractValidator<UpdateInterestInput>
    {
        public UpdateInterestInputValidator()
        {
            RuleFor(interest => interest.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
