using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("UpdateLocationInput")]
    [GraphQLDescription("Input type for updating an existing location.")]
    public class UpdateLocationInput
    {
        public UpdateLocationInput(string name, string address)
        {
            Name = name;
            Address = address;
        }

        [GraphQLDescription("The name of the location.")]
        public string Name { get; set; }

        [GraphQLDescription("The address of the location.")]
        public string Address { get; set; }
    }

    public class UpdateLocationInputValidator : AbstractValidator<UpdateLocationInput>
    {
        public UpdateLocationInputValidator()
        {
            RuleFor(location => location.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(location => location.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}
