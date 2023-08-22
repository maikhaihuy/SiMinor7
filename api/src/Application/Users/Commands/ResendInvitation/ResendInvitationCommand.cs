using MediatR;
using Microsoft.AspNetCore.Identity;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Application.Users.EventHandlers.SendInvitation;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Users.Commands.ResendInvitation;

public record ResendInvitationCommand(string Id) : IRequest;

public class ResendInvitationCommandHandler : IRequestHandler<ResendInvitationCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPublisher _publisher;

    public ResendInvitationCommandHandler(
        UserManager<ApplicationUser> userManager,
        IPublisher publisher)
    {
        _userManager = userManager;
        _publisher = publisher;
    }

    public async Task Handle(ResendInvitationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException($"{App.ResponseCodeMessage.AccountNotExists}", $"The resource you have requested cannot be found");
        }

        if (user.Status == Domain.Enums.UserStatus.Deactivated)
        {
            throw new ForbiddenAccessException($"{App.ResponseCodeMessage.AccountDeactive}. Your account has been deactivated.");
        }

        await _publisher.Publish(new SendInvitationEvent(user), cancellationToken);
    }
}