using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class UpdateLocationCommand
    {
        public UpdateLocationCommand(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(location => location.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(location => location.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}
