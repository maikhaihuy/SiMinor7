using FluentValidation;

namespace SiMinor7.Application.Users.Commands.SendInvitation;

public class SendInvitationCommandValidator : AbstractValidator<SendInvitationCommand>
{
    public SendInvitationCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .MaximumLength(256)
            .NotEmpty();

        RuleFor(v => v.LastName)
            .MaximumLength(256)
            .NotEmpty();

        RuleFor(v => v.Email)
            .MaximumLength(256)
            .NotEmpty();

        RuleFor(u => u.RoleId)
            .NotEmpty();
    }
}