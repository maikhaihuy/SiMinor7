using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiMinor7.Application.Auth.Shared.Models;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Auth.Commands.RefreshToken;
public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public record RefreshTokenCommand : IRequest<AuthResponse>
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
        if (principal is null)
        {
            throw new UnauthorizedAccessException(MessageCode.ExpiredCredential);
        }
        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null || user.Status != Domain.Enums.UserStatus.Actived)
        {
            throw new UnauthorizedAccessException(MessageCode.InvalidCredential);
        }

        if (!user.EmailConfirmed)
        {
            throw new InvalidRequestException(MessageCode.IncompleteAccount);
        }

        if (user.Status == Domain.Enums.UserStatus.Blocked)
        {
            throw new InvalidRequestException(MessageCode.BlockedAccount);
        }

        if (_tokenService.IsRefreshTokenExpired(command.RefreshToken))
        {
            throw new InvalidRequestException(MessageCode.ExpiredToken);
        }

        var userLogin = await _dbContext.SessionLogins.Where(ul => ul.UserId == userId && ul.RefreshToken == command.RefreshToken).FirstOrDefaultAsync();

        if (userLogin is null)
        {
            throw new InvalidRequestException(MessageCode.InvalidToken);
        }

        var userClaims = await _userManager.GetClaimsAsync(user) ?? new List<Claim>();
        var roles = await _userManager.GetRolesAsync(user) ?? new List<string>();

        var accessToken = _tokenService.GenerateAccessToken(user, userClaims, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        userLogin.RefreshToken = refreshToken;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResponse
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Role = roles.First()
        };
    }
}