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
}
