﻿using FluentValidation;
using System;

namespace CandidateJourney.Application.Contracts.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand(Guid registrationId, string password)
        {
            RegistrationId = registrationId;
            Password = password;
        }

        public Guid RegistrationId { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.RegistrationId).NotEmpty().WithMessage("Registration ID is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
