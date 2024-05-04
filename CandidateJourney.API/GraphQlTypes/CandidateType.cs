using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("Candidate")]
    public class CandidateType : ObjectType<Candidate>
    {
        protected override void Configure(IObjectTypeDescriptor<Candidate> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.FirstName).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.LastName).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Email).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.PhoneNumber).Type<StringType>();
            descriptor.Field(c => c.Specialization).Type<StringType>();
            descriptor.Field(c => c.DateOfGraduation).Type<DateTimeType>();
            descriptor.Field(c => c.CandidateType).Type<NonNullType<EnumType<CandidateIntent>>>();
            descriptor.Field(c => c.GraduationType).Type<NonNullType<EnumType<AcademicDegree>>>();
            descriptor.Field(c => c.PictureName).Type<StringType>();
            descriptor.Field(c => c.Interests).Type<ListType<NonNullType<StringType>>>();
            descriptor.Field(c => c.ContactHistories).Type<ListType<NonNullType<ObjectType<ContactHistory>>>>();
            descriptor.Field(c => c.ExtraInfo).Type<StringType>();
        }
    }
}
