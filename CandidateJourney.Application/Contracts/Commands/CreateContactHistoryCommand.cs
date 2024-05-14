using FluentValidation;
using System;
using System.Collections.Generic;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateContactHistoryCommand
    {
        public CreateContactHistoryCommand(string sendgridTemplateId, List<Guid> candidateId, Guid eventId)
        {
            SendgridTemplateId = sendgridTemplateId;
            CandidateId = candidateId ?? new List<Guid>();
            EventId = eventId;
        }

        public string SendgridTemplateId { get; set; }
        public List<Guid> CandidateId { get; set; }
        public Guid EventId { get; set; }
    }

    public class CreateContactHistoryCommandValidator : AbstractValidator<CreateContactHistoryCommand>
    {
        public CreateContactHistoryCommandValidator()
        {
            RuleFor(c => c.CandidateId).NotEmpty().WithMessage("CandidateId is required");
            RuleFor(c => c.SendgridTemplateId).NotEmpty().WithMessage("SendgridTemplateId is required");
            RuleFor(c => c.EventId).NotEmpty().WithMessage("EventId is required");
        }
    }
}
