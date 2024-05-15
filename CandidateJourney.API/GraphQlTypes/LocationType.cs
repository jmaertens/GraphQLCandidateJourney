using CandidateJourney.Domain;
using HotChocolate.Types;
using Location = CandidateJourney.Domain.Location;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("Location")]
    public class LocationType : ObjectType<Location>
    {
        protected override void Configure(IObjectTypeDescriptor<Location> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("A location where events are held.");

            descriptor.Field(l => l.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the location");

            descriptor.Field(l => l.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the location");

            descriptor.Field(l => l.Address)
                .Type<NonNullType<StringType>>()
                .Description("The address of the location");

            descriptor.Field(l => l.Events)
                .Type<ListType<NonNullType<ObjectType<Event>>>>()
                .Description("The events held at this location");
        }
    }
}
