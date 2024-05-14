using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("User")]
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("A user of the system");

            descriptor.Field(u => u.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the user");

            descriptor.Field(u => u.FirstName)
                .Type<NonNullType<StringType>>()
                .Description("The first name of the user");

            descriptor.Field(u => u.LastName)
                .Type<NonNullType<StringType>>()
                .Description("The last name of the user");

            descriptor.Field(u => u.EmailAddress)
                .Type<NonNullType<StringType>>()
                .Description("The email address of the user");

            descriptor.Field(u => u.IsRegistered)
                .Type<NonNullType<BooleanType>>()
                .Description("Indicates if the user is registered");

            descriptor.Field(u => u.IsDeleted)
                .Type<NonNullType<BooleanType>>()
                .Description("Indicates if the user is deleted");
        }
    }
}
