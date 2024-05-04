using CandidateJourney.Domain;

namespace API.GraphQlTypes
{
    [GraphQLName("CandidateIntent")]
    public class CandidateIntentType : EnumType<CandidateIntent>
    {
        protected override void Configure(IEnumTypeDescriptor<CandidateIntent> descriptor)
        {
            descriptor.BindValuesExplicitly();
            descriptor.Value(CandidateIntent.Internship).Name("INTERNSHIP");
            descriptor.Value(CandidateIntent.Job).Name("JOB");
            descriptor.Value(CandidateIntent.Information).Name("INFORMATION");
        }
    }
}
