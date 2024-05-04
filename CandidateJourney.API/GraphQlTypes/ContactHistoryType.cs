using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("ContactHistory")]
    public class ContactHistoryType : ObjectType<ContactHistory>
    {
        protected override void Configure(IObjectTypeDescriptor<ContactHistory> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(ch => ch.Id).Type<NonNullType<IdType>>();
            descriptor.Field(ch => ch.CreatedBy).Type<NonNullType<ObjectType<User>>>();
            descriptor.Field(ch => ch.CreatedOn).Type<NonNullType<DateTimeType>>();
        }
    }
}
