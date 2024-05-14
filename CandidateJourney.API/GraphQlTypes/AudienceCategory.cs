using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("AudienceCategory")]
    public class AudienceCategoryType : EnumType<AudienceCategory>
    {
        protected override void Configure(IEnumTypeDescriptor<AudienceCategory> descriptor)
        {
            descriptor.BindValuesExplicitly();

            descriptor.Description("Defines the target audience category for an event.");
            
            descriptor.Value(AudienceCategory.Student)
                .Name("STUDENT")
                .Description("The event is targeted towards students.");

            descriptor.Value(AudienceCategory.All)
                .Name("ALL")
                .Description("The event is open to all audiences.");
        }
    }
}
