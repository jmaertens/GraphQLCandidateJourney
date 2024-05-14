using CandidateJourney.Domain;

namespace CandidateJourney.API.GraphQLTypes
{
    [GraphQLName("Candidate")]
    public class CandidateType : ObjectType<Candidate>
    {
        protected override void Configure(IObjectTypeDescriptor<Candidate> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Description("A potential candidate from an event.");

            descriptor.Field(c => c.Id)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the candidate");

            descriptor.Field(c => c.FirstName)
                .Type<NonNullType<StringType>>()
                .Description("The first name of the candidate");

            descriptor.Field(c => c.LastName)
                .Type<NonNullType<StringType>>()
                .Description("The last name of the candidate");

            descriptor.Field(c => c.Email)
                .Type<NonNullType<StringType>>()
                .Description("The email address of the candidate");

            descriptor.Field(c => c.PhoneNumber)
                .Type<StringType>()
                .Description("The phone number of the candidate");

            descriptor.Field(c => c.Specialization)
                .Type<StringType>()
                .Description("The specialization of the candidate");

            descriptor.Field(c => c.DateOfGraduation)
                .Type<DateTimeType>()
                .Description("The graduation date of the candidate");

            descriptor.Field(c => c.CandidateType)
                .Type<NonNullType<EnumType<CandidateIntent>>>()
                .Description("The type of the candidate");

            descriptor.Field(c => c.GraduationType)
                .Type<NonNullType<EnumType<AcademicDegree>>>()
                .Description("The graduation degree of the candidate");

            descriptor.Field(c => c.PictureName)
                .Type<StringType>()
                .Description("The name of the candidate's picture");

            descriptor.Field(c => c.Interests)
                .Type<ListType<NonNullType<StringType>>>()
                .Description("The interests of the candidate");

            descriptor.Field(c => c.ContactHistories)
                .Type<ListType<NonNullType<ObjectType<ContactHistory>>>>()
                .Description("The contact histories of the candidate");

            descriptor.Field(c => c.ExtraInfo)
                .Type<StringType>()
                .Description("Any extra information about the candidate");
        }
    }
}
