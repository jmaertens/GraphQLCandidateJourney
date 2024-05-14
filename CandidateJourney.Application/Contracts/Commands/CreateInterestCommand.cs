﻿using FluentValidation;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class CreateInterestCommand
    {
        public CreateInterestCommand(string name)
        {
            Name = name;
        }

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
