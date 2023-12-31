﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Settings;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Domain.Entities;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Application.Users.Commands.ChangeStatus;

public record ChangeStatusCommand(string Id, UserStatus Status) : IRequest;

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand>
{
    private const string TemplateName = "update-user-status.liquid";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly SystemSettings _systemSettings;

    public ChangeStatusCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService, IOptions<SystemSettings> optionSystemSettings)
    {
        _userManager = userManager;
        _emailService = emailService;
        _systemSettings = optionSystemSettings.Value;
    }

    public async Task Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException(MessageCode.AccountNotExists);
        }

        _emailService.SendEmail(user.Email, _systemSettings.ResetPasswordEmailSubject, TemplateName, new
        {
            Status = $"{user.Status}"
        });
    }
}