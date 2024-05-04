using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("AudienceCategory")]
    public class AudienceCategoryType : EnumType<AudienceCategory>
    {
        protected override void Configure(IEnumTypeDescriptor<AudienceCategory> descriptor)
        {
            descriptor.BindValuesExplicitly();
            descriptor.Value(AudienceCategory.Student).Name("STUDENT");
            descriptor.Value(AudienceCategory.All).Name("ALL");
        }
    }
}