using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Settings;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Users.EventHandlers.SendInvitation;

public record SendInvitationEvent(ApplicationUser User) : INotification;

public class SendInvitationEventHandler : INotificationHandler<SendInvitationEvent>
{
    private const string TemplateName = "send-invitation.liquid";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SystemSettings _systemSettings;
    private readonly IEmailService _emailService;

    public SendInvitationEventHandler(
        UserManager<ApplicationUser> userManager,
        IOptions<SystemSettings> systemSettings,
        IEmailService emailService)
    {
        _userManager = userManager;
        _systemSettings = systemSettings.Value;
        _emailService = emailService;
    }

    public async Task Handle(SendInvitationEvent notification, CancellationToken cancellationToken)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(notification.User);
        string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        string setPasswordUrl = $"{_systemSettings.ClientAppBaseUrl}{_systemSettings.ClientAppSetPasswordPath}";
        IDictionary<string, string?> queryStrings = new Dictionary<string, string?>
            {
                { "userId", notification.User.Id.ToString() },
                { "token", encodedToken },
                { "purpose", UserManager<ApplicationUser>.ConfirmEmailTokenPurpose }
            };
        string invitationLink = QueryHelpers.AddQueryString(setPasswordUrl, queryStrings);

        _emailService.SendEmail(notification.User.Email, _systemSettings.InvitationEmailSubject, TemplateName, new
        {
            InvitationLink = invitationLink
        });
    }
}