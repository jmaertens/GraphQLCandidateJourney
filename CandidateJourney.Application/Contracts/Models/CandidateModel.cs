using CandidateJourney.Domain;
using System;
using System.Collections.Generic;

namespace CandidateJourney.Application.Contracts.Models
{
    public class CandidateModel
    {
        public CandidateModel(Guid id, string firstName, string lastName, string email, CandidateIntent candidateType, AcademicDegree graduationType,
            string? phoneNumber = null, string? specialization = null, DateTime? dateOfGraduation = null,
            string? pictureName = null, string? extraInfo = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CandidateType = candidateType;
            GraduationType = graduationType;
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
            ExtraInfo = extraInfo;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public DateTime? DateOfGraduation { get; set; }
        public CandidateIntent CandidateType { get; set; }
        public AcademicDegree GraduationType { get; set; }
        public string? ExtraInfo { get; set; }
    }
}
