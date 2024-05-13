using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateContactHistoryCommand
    {
        public string SendgridTemplateId { get; set; }
        public List<Guid> CandidateId { get; set; }
        public Guid EventId { get; set; }
        public CreateContactHistoryCommand()
        {
            CandidateId = new List<Guid>();
        }
    }

    public class CreateContactHistoryCommandValidator : AbstractValidator<CreateContactHistoryCommand>
    {
        public CreateContactHistoryCommandValidator()
        {
            RuleFor(c => c.CandidateId).NotEmpty().WithMessage("CandidateId is required");
            RuleFor(c => c.SendgridTemplateId).NotEmpty().WithMessage("SengridId is required");
            RuleFor(c => c.EventId).NotEmpty().WithMessage("EventId is required");
        }
    }
}