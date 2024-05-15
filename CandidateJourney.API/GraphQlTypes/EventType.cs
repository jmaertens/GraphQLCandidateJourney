using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("Event")]
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("An event which is organized.");

            descriptor.Field(e => e.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the event");

            descriptor.Field(e => e.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the event");

            descriptor.Field(e => e.Description)
                .Type<StringType>()
                .Description("The description of the event");

            descriptor.Field(e => e.Organizer)
                .Type<NonNullType<StringType>>()
                .Description("The organizer of the event");
        
            descriptor.Field(e => e.EventLink)
                .Type<StringType>()
                .Description("The link to the event");

            descriptor.Field(e => e.StartDateTime)
                .Type<NonNullType<DateTimeType>>()
                .Description("The start time of the event");

            descriptor.Field(e => e.EndDateTime)
                .Type<DateTimeType>()
                .Description("The end time of the event");

            descriptor.Field(e => e.TargetAudience)
                .Type<NonNullType<EnumType<AudienceCategory>>>()
                .Description("The target audience of the event");

            descriptor.Field(e => e.Candidates)
                .Type<ListType<NonNullType<ObjectType<Candidate>>>>()
                .Description("The candidates for the event");

            descriptor.Field(e => e.CreatedBy)
                .Type<NonNullType<ObjectType<User>>>()
                .Description("The user who created the event");

            descriptor.Field(e => e.CreatedOn)
                .Type<NonNullType<DateTimeType>>()
                .Description("The creation date of the event");

            descriptor.Field(e => e.UpdatedBy)
                .Type<ObjectType<User>>()
                .Description("The user who updated the event");

            descriptor.Field(e => e.UpdatedOn)
                .Type<DateTimeType>()
                .Description("The update date of the event");

            descriptor.Field(e => e.IsDeleted)
                .Type<NonNullType<BooleanType>>()
                .Description("Indicates whether the event is deleted");
        }
    }
}
