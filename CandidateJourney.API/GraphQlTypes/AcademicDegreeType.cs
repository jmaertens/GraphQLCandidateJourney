using CandidateJourney.Domain;
using HotChocolate.Types;

namespace API.GraphQlTypes
{
    [GraphQLName("AcademicDegree")]
    public class AcademicDegreeType : EnumType<AcademicDegree>
    {
        protected override void Configure(IEnumTypeDescriptor<AcademicDegree> descriptor)
        {
            descriptor.Description("Represents the academic degree obtained by a candidate.");

            descriptor.BindValuesExplicitly();

            descriptor.Value(AcademicDegree.None)
                .Name("NONE")
                .Description("The candidate has not obtained any academic degree.");

            descriptor.Value(AcademicDegree.Bachelor)
                .Name("BACHELOR")
                .Description("The candidate has obtained a Bachelor's degree.");

            descriptor.Value(AcademicDegree.Master)
                .Name("MASTER")
                .Description("The candidate has obtained a Master's degree.");

            descriptor.Value(AcademicDegree.Graduate)
                .Name("GRADUATE")
                .Description("The candidate has obtained a Graduate degree.");

            descriptor.Value(AcademicDegree.Doctorate)
                .Name("DOCTORATE")
                .Description("The candidate has obtained a Doctorate degree.");
        }
    }
}
