using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateInterestCommand
    {
        public string Name { get; set; }
    }

    public class CreateInterestCommandValidator : AbstractValidator<CreateInterestCommand>
    {
        public CreateInterestCommandValidator()
        {
            RuleFor(newInterest => newInterest.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
