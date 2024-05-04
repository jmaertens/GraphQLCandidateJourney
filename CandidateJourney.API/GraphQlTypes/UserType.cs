using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQlTypes
{
    [GraphQLName("User")]
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
            descriptor.Field(u => u.FirstName).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.LastName).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.EmailAddress).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.IsRegistered).Type<NonNullType<BooleanType>>();
            descriptor.Field(u => u.IsDeleted).Type<NonNullType<BooleanType>>();
        }
    }
}
