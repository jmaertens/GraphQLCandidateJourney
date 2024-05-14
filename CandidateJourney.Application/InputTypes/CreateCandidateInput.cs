using CandidateJourney.Domain;
using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("CreateCandidateInput")]
    [GraphQLDescription("Input type for creating a new candidate.")]
    public class CreateCandidateInput
    {
        public CreateCandidateInput(
            string firstName,
            string lastName,
            string email,
            CandidateIntent candidateType,
            AcademicDegree graduationType,
            List<string> interests,
            string? phoneNumber = null,
            string? specialization = null,
            DateTime? dateOfGraduation = null,
            string? extraInfo = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CandidateType = candidateType;
            GraduationType = graduationType;
            Interests = interests ?? new List<string>();
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
            ExtraInfo = extraInfo;
        }

        [GraphQLDescription("The first name of the candidate.")]
        public string FirstName { get; set; }

        [GraphQLDescription("The last name of the candidate.")]
        public string LastName { get; set; }

        [GraphQLDescription("The email address of the candidate.")]
        public string Email { get; set; }

        [GraphQLDescription("The phone number of the candidate (optional).")]
        public string? PhoneNumber { get; set; }

        [GraphQLDescription("The specialization of the candidate (optional).")]
        public string? Specialization { get; set; }

        [GraphQLDescription("The graduation date of the candidate (optional).")]
        public DateTime? DateOfGraduation { get; set; }

        [GraphQLDescription("The type of the candidate's intent.")]
        public CandidateIntent CandidateType { get; set; }

        [GraphQLDescription("The academic degree of the candidate.")]
        public AcademicDegree GraduationType { get; set; }

        [GraphQLDescription("The interests of the candidate.")]
        public List<string> Interests { get; set; }

        [GraphQLDescription("Any extra information about the candidate (optional).")]
        public string? ExtraInfo { get; set; }
    }


    public class CreateCandidateInputValidator : AbstractValidator<CreateCandidateInput>
    {
        public CreateCandidateInputValidator()
        {
            RuleFor(candidate => candidate.FirstName)
                .NotEmpty().WithMessage("First Name is required");

            RuleFor(candidate => candidate.LastName)
                .NotEmpty().WithMessage("Last Name is required");

            RuleFor(candidate => candidate.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address");
            
            RuleFor(candidate => candidate.CandidateType)
                .IsInEnum().WithMessage("Candidate Type must be a valid enum value");

            RuleFor(candidate => candidate.GraduationType)
                .IsInEnum().WithMessage("Graduation Type must be a valid enum value");

            RuleFor(candidate => candidate.DateOfGraduation)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of Graduation cannot be in the future")
                .When(candidate => candidate.DateOfGraduation.HasValue);
        }
    }
}
