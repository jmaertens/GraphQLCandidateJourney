using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("CreateLocationInput")]
    [GraphQLDescription("Input type for creating a new location.")]
    public class CreateLocationInput
    {
        public CreateLocationInput(string name, string address)
        {
            Name = name;
            Address = address;
        }

        [GraphQLDescription("The name of the location.")]
        public string Name { get; set; }

        [GraphQLDescription("The address of the location.")]
        public string Address { get; set; }
    }

    public class CreateLocationInputValidator : AbstractValidator<CreateLocationInput>
    {
        public CreateLocationInputValidator()
        {
            RuleFor(location => location.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(location => location.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}
