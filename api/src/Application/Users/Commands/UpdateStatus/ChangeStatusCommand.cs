using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Settings;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Domain.Entities;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Application.Users.Commands.UpdateStatus;

public record UpdateStatusCommand(string Id, UserStatus Status) : IRequest;

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly SystemSettings _systemSettings;

    public UpdateStatusCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService, IOptions<SystemSettings> optionSystemSettings)
    {
        _userManager = userManager;
        _emailService = emailService;
        _systemSettings = optionSystemSettings.Value;
    }

    public async Task Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException($"{App.ResponseCodeMessage.AccountNotExists}", $"The resource you have requested cannot be found");
        }

        _emailService.SendEmail(user.Email, _systemSettings.ResetPasswordEmailSubject, "", new
        {
        });
    }
}