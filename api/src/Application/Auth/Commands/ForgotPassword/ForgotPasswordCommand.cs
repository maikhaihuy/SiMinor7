using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Settings;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly SystemSettings _systemSettings;

    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService, IOptions<SystemSettings> optionSystemSettings)
    {
        _userManager = userManager;
        _emailService = emailService;
        _systemSettings = optionSystemSettings.Value;
    }

    public async Task Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            // throw new NotFoundException(App.MessageCode.AccountNotExists);
            return;
        }

        if (!user.EmailConfirmed)
        {
            throw new InvalidRequestException(MessageCode.IncompleteAccount);
        }

        if (user.Status == Domain.Enums.UserStatus.Blocked)
        {
            throw new InvalidRequestException(MessageCode.BlockedAccount);
        }

        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        IDictionary<string, string?> queryStrings = new Dictionary<string, string?>
            {
                { "userId", user.Id.ToString() },
                { "token", encodedToken },
                { "purpose", UserManager<ApplicationUser>.ResetPasswordTokenPurpose }
            };

        string setPasswordUrl = $"{_systemSettings.ClientAppBaseUrl}{_systemSettings.ClientAppSetPasswordPath}";
        string passwordResetLink = QueryHelpers.AddQueryString(setPasswordUrl, queryStrings);

        _emailService.SendEmail(command.Email, _systemSettings.ResetPasswordEmailSubject, "", new
        {
            PasswordResetLink = passwordResetLink
        });
    }
}