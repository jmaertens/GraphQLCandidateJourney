using CandidateJourney.Domain;

namespace API.GraphQlTypes
{
    [GraphQLName("Interest")]
    public class InterestType : ObjectType<Interest>
    {
        protected override void Configure(IObjectTypeDescriptor<Interest> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("Interest of a candidate.");

            descriptor.Field(i => i.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the interest");

            descriptor.Field(i => i.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the interest");
        }
    }
}
