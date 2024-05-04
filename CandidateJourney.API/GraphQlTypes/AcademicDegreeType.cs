using CandidateJourney.Domain;

namespace API.GraphQlTypes
{
    [GraphQLName("AcademicDegree")]
    public class AcademicDegreeType : EnumType<AcademicDegree>
    {
        protected override void Configure(IEnumTypeDescriptor<AcademicDegree> descriptor)
        {
            descriptor.BindValuesExplicitly();
            descriptor.Value(AcademicDegree.None).Name("NONE");
            descriptor.Value(AcademicDegree.Bachelor).Name("BACHELOR");
            descriptor.Value(AcademicDegree.Master).Name("MASTER");
            descriptor.Value(AcademicDegree.Graduate).Name("GRADUATE");
            descriptor.Value(AcademicDegree.Doctorate).Name("DOCTORATE");
        }
    }
}
