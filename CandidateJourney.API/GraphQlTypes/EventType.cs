using CandidateJourney.Domain;
using CandidateJourney.Infrastructure;
using Microsoft.EntityFrameworkCore;

using Location = CandidateJourney.Domain.Location;

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

            descriptor.Field(e => e.CreatedBy)
                .Type<NonNullType<ObjectType<User>>>()
                .Description("The user who created the event");

            descriptor.Field(e => e.Candidates)
                .Type<ListType<NonNullType<ObjectType<Candidate>>>>()
                .Description("The candidates for the event");
            
            descriptor.Field("locations")
                .ResolveWith<EventLocationResolver>(e => e.ResolveLocationAsync(default!, default!, default))
                .Type<ListType<NonNullType<ObjectType<Location>>>>()
                .Description("The locations of the event");
        }
    }
    
    public class EventLocationResolver
    {
        public async Task<IEnumerable<Location>> ResolveLocationAsync([Parent] Event @event, ILocationsDataLoader dataLoader, CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(@event.Id, cancellationToken);
        }

        [DataLoader]
        internal static async Task<IReadOnlyDictionary<Guid, IEnumerable<Location>>> GetLocations(IReadOnlyList<Guid> eventIds, IDbContextFactory<CandidateJourneyDbContext> contextFactory, CancellationToken cancellationToken)
        {
            await using var context = contextFactory.CreateDbContext();
            var eventsWithLocations = await context.Events
                                                   .Where(e => eventIds.Contains(e.Id))
                                                   .Include(e => e.Locations)
                                                   .ToListAsync(cancellationToken);

            return eventsWithLocations.ToDictionary(
               @event => @event.Id,
               @event => @event.Locations.AsEnumerable());
        }
    }
}
