﻿using FluentValidation;

namespace SiMinor7.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(v => v.Email)
            .MaximumLength(256)
            .NotEmpty()
            .EmailAddress();
    }
}