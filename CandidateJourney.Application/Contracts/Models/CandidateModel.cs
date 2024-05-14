using CandidateJourney.Domain;
using System;
using System.Collections.Generic;

namespace CandidateJourney.Application.Contracts.Models
{
    public class CandidateModel
    {
        public CandidateModel(Guid id, string firstName, string lastName, string email, CandidateIntent candidateType, AcademicDegree graduationType,
            List<string> interests, string? phoneNumber = null, string? specialization = null, DateTime? dateOfGraduation = null,
            string? pictureName = null, string? extraInfo = null, List<ContactHistoryModel>? contactHistories = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CandidateType = candidateType;
            GraduationType = graduationType;
            Interests = interests ?? new List<string>();
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            DateOfGraduation = dateOfGraduation;
            PictureName = pictureName;
            ExtraInfo = extraInfo;
            ContactHistories = contactHistories ?? new List<ContactHistoryModel>();
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
        public string? PictureName { get; set; }
        public List<string> Interests { get; set; }
        public string? ExtraInfo { get; set; }
        public List<ContactHistoryModel> ContactHistories { get; set; }
    }
}
