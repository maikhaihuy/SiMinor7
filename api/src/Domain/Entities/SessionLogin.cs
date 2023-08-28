using SiMinor7.Domain.Common;

namespace SiMinor7.Domain.Entities;
public class SessionLogin : BaseAuditableEntity
{
    public string RefreshToken { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTimeOffset? Revoked { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = new();
}