using CandidateJourney.Domain;

namespace API.GraphQlTypes
{
    [GraphQLName("CandidateIntent")]
    public class CandidateIntentType : EnumType<CandidateIntent>
    {
        protected override void Configure(IEnumTypeDescriptor<CandidateIntent> descriptor)
        {
            descriptor.BindValuesExplicitly();

            descriptor.Description("Indicates the intent of a candidate, such as looking for an internship, job, or just information.");

            descriptor.Value(CandidateIntent.Internship)
                .Name("INTERNSHIP")
                .Description("The candidate is looking for an internship position.");

            descriptor.Value(CandidateIntent.Job)
                .Name("JOB")
                .Description("The candidate is looking for a job.");

            descriptor.Value(CandidateIntent.Information)
                .Name("INFORMATION")
                .Description("The candidate is seeking information.");
        }
    }
}
