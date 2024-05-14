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
}
