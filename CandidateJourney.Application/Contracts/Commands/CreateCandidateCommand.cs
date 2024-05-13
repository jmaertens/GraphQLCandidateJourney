using CandidateJourney.Domain;
using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateCandidateCommand
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public CandidateIntent CandidateType { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
        public string? Specialization { get; set; }
        public DateTime? DateOfGraduation { get; set; }
        public string? PictureData { get; set; }
        public List<string> Interests { get; set; }
        public string? ExtraInfo { get; set; }
    }

    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateCommandValidator()
        {
            RuleFor(candidate => candidate.FirstName).NotEmpty().WithMessage("Firstname is required");
            RuleFor(candidate => candidate.LastName).NotEmpty().WithMessage("Lastname is required");
            RuleFor(candidate => candidate.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(candidate => candidate.CandidateType).IsInEnum();
            RuleFor(candidate => candidate.AcademicDegree).IsInEnum();
            RuleFor(candidate => candidate.Interests).NotEmpty().WithMessage("There has to be at least one Interest");
        }
    }
}