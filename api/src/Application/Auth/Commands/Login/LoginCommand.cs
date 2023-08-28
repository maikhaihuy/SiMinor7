using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SiMinor7.Application.Auth.Shared.Models;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsRemember { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<AuthResponse> Handle(LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(loginCommand.Email);
        if (user is null)
        {
            throw new UnauthorizedAccessException(MessageCode.InvalidCredential);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginCommand.Password);
        if (!authenticated)
        {
            throw new UnauthorizedAccessException(MessageCode.InvalidCredential);
        }

        if (!user.EmailConfirmed)
        {
            throw new ForbiddenAccessException(MessageCode.IncompleteAccount);
        }

        var claims = await _userManager.GetClaimsAsync(user) ?? new List<Claim>();
        var roles = await _userManager.GetRolesAsync(user) ?? new List<string>();

        string accessToken = _tokenService.GenerateAccessToken(user, claims, roles);
        string refreshToken = string.Empty;

        if (loginCommand.IsRemember)
        {
            refreshToken = _tokenService.GenerateRefreshToken();

            // Add new refresh token
            var userLogin = new SessionLogin
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                UserAgent = _currentUserService.UserAgent
            };
            _dbContext.SessionLogins.Add(userLogin);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var result = new AuthResponse
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Role = roles.First()
        };

        return result;
    }
}