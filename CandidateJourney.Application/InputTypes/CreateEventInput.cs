using CandidateJourney.Domain;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("CreateEventInput")]
    [GraphQLDescription("Input type for creating a new event.")]
    public class CreateEventInput
    {
        public CreateEventInput(
            string name,
            string description,
            string organizer,
            string location,
            string eventLink,
            DateTime startDateTime,
            AudienceCategory targetAudience,
            DateTime? endDateTime = null)
        {
            Name = name;
            Description = description;
            Organizer = organizer;
            Location = location;
            EventLink = eventLink;
            StartDateTime = startDateTime;
            TargetAudience = targetAudience;
            EndDateTime = endDateTime;
        }

        [GraphQLDescription("The name of the event.")]
        public string Name { get; set; }

        [GraphQLDescription("The description of the event.")]
        public string Description { get; set; }

        [GraphQLDescription("The organizer of the event.")]
        public string Organizer { get; set; }

        [GraphQLDescription("The location of the event.")]
        public string Location { get; set; }

        [GraphQLDescription("The link to the event.")]
        public string EventLink { get; set; }

        [GraphQLDescription("The start date and time of the event.")]
        public DateTime StartDateTime { get; set; }

        [GraphQLDescription("The end date and time of the event (optional).")]
        public DateTime? EndDateTime { get; set; }

        [GraphQLDescription("The target audience of the event.")]
        public AudienceCategory TargetAudience { get; set; }
    }
}
