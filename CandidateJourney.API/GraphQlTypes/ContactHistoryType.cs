using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("ContactHistory")]
    public class ContactHistoryType : ObjectType<ContactHistory>
    {
        protected override void Configure(IObjectTypeDescriptor<ContactHistory> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("Contact history of a candidate.");

            descriptor.Field(ch => ch.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the contact history");

            descriptor.Field(ch => ch.CreatedBy)
                .Type<NonNullType<ObjectType<User>>>()
                .Description("The user who created the contact history");

            descriptor.Field(ch => ch.CreatedOn)
                .Type<NonNullType<DateTimeType>>()
                .Description("The creation date of the contact history");
        }
    }
}
