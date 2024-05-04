using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("Event")]
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(e => e.Id).Type<NonNullType<IdType>>();
            descriptor.Field(e => e.Name).Type<NonNullType<StringType>>();
            descriptor.Field(e => e.Description).Type<StringType>();
            descriptor.Field(e => e.Organizer).Type<NonNullType<StringType>>();
            descriptor.Field(e => e.Location).Type<NonNullType<StringType>>();
            descriptor.Field(e => e.EventLink).Type<StringType>();
            descriptor.Field(e => e.StartDateTime).Type<NonNullType<DateTimeType>>();
            descriptor.Field(e => e.EndDateTime).Type<DateTimeType>();
            descriptor.Field(e => e.TargetAudience).Type<NonNullType<EnumType<AudienceCategory>>>();
            descriptor.Field(e => e.Candidates).Type<ListType<NonNullType<ObjectType<Candidate>>>>();
            descriptor.Field(e => e.CreatedBy).Type<NonNullType<ObjectType<User>>>();
            descriptor.Field(e => e.CreatedOn).Type<NonNullType<DateTimeType>>();
            descriptor.Field(e => e.UpdatedBy).Type<ObjectType<User>>();
            descriptor.Field(e => e.UpdatedOn).Type<DateTimeType>();
            descriptor.Field(e => e.IsDeleted).Type<NonNullType<BooleanType>>();
        }
    }
}
