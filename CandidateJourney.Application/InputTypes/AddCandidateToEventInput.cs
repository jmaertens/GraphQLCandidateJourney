using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Migrations;
using FluentValidation;
using HotChocolate;

namespace Application.InputTypes
{
    [GraphQLName("AddCandidateToEventInput")]
    [GraphQLDescription("Input type for adding a candidate to an event.")]
    public class AddCandidateToEventInput
    {
        [GraphQLDescription("The first name of the candidate.")]
        public string FirstName { get; set; }

        [GraphQLDescription("The last name of the candidate.")]
        public string LastName { get; set; }

        [GraphQLDescription("The email of the candidate.")]
        public string Email { get; set; }

        [GraphQLDescription("The phone number of the candidate.")]
        public string PhoneNumber { get; set; }

        [GraphQLDescription("The candidate's type.")]
        public CandidateIntent CandidateType { get; set; }

        [GraphQLDescription("The candidate's academic degree.")]
        public AcademicDegree AcademicDegree { get; set; }
        
        [GraphQLDescription("The specialization of the candidate (optional).")]
        public string? Specialization { get; set; }

        [GraphQLDescription("The date of graduation of the candidate (optional).")]
        public DateTime? DateOfGraduation { get; set; }

        [GraphQLDescription("Additional information about the candidate (optional).")]
        public string? ExtraInfo { get; set; }
    }

    public class AddCandidateToEventInputValidator : AbstractValidator<AddCandidateToEventInput>
    {
        public AddCandidateToEventInputValidator()
        {
            RuleFor(input => input.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(input => input.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(input => input.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(input => input.CandidateType).IsInEnum().WithMessage("Invalid candidate type.");
            RuleFor(input => input.AcademicDegree).IsInEnum().WithMessage("Invalid academic degree.");
        }
    }
}
