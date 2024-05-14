using CandidateJourney.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateCandidateCommand
    {
        public CreateCandidateCommand(
            string firstName,
            string lastName,
            string email,
            CandidateIntent candidateType,
            AcademicDegree academicDegree,
            List<string> interests,
            string phoneNumber,
            string? specialization = null,
            DateTime? dateOfGraduation = null,
            string? pictureData = null,
            string? extraInfo = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CandidateType = candidateType;
            AcademicDegree = academicDegree;
            Interests = interests ?? new List<string>();
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
            PictureData = pictureData;
            ExtraInfo = extraInfo;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
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
            RuleFor(candidate => candidate.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(candidate => candidate.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(candidate => candidate.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(candidate => candidate.CandidateType).IsInEnum();
            RuleFor(candidate => candidate.AcademicDegree).IsInEnum();
            RuleFor(candidate => candidate.Interests).NotEmpty().WithMessage("There has to be at least one interest");
        }
    }
}
