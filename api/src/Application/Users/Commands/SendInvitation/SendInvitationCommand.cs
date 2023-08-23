using System.Net.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Domain.Entities;
using SiMinor7.Application.Users.EventHandlers.SendInvitation;

namespace SiMinor7.Application.Users.Commands.SendInvitation;

public record SendInvitationCommand : IRequest<string>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
}

public class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPublisher _publisher;

    public SendInvitationCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IPublisher publisher)
    {
        _context = context;
        _userManager = userManager;
        _publisher = publisher;
    }

    public async Task<string> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new InvalidRequestException(MessageCode.EmailExisted);
        }

        var addr = new MailAddress(request.Email);
        var newUser = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = addr.User,
            DateCreated = DateTimeOffset.Now
        };

        var result = await _userManager.CreateAsync(newUser);
        if (!result.Succeeded)
        {
            throw new Exception($"{result.Errors.First().Code}\n ${result.Errors.First().Description}");
        }

        var userRole = new ApplicationUserRole
        {
            RoleId = request.RoleId,
            UserId = newUser.Id
        };
        await _context.UserRoles.AddAsync(userRole, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new SendInvitationEvent(newUser), cancellationToken);

        return newUser.Id;
    }
}