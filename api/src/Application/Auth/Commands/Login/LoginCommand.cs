using System.Security.Claims;
using MediatR;
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
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
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

        //if (loginCommand.IsRemember)
        //{
        //    refreshToken = _jwtService.GenerateRefreshToken();

        //    // Update refresh token
        //    user.RefreshToken = refreshToken;
        //    await _userManager.UpdateAsync(user);
        //}

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