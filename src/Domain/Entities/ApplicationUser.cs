using Microsoft.AspNetCore.Identity;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Avatar { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public UserStatus Status { get; set; }

    public virtual ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
}