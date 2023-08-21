using System.Security.Claims;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Common.Interfaces;

public interface ITokenService : IScopedService
{
    string GenerateAccessToken(ApplicationUser user, IList<Claim> userClaims, IList<string> roles);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    string GenerateRefreshToken(int length = 32);

    bool IsRefreshTokenExpired(string refreshToken);
}