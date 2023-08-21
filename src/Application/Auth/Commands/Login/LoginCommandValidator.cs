using FluentValidation;

namespace SiMinor7.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.Email)
            .MaximumLength(256)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.Password)
            .MinimumLength(8)
            .NotEmpty();
    }
}