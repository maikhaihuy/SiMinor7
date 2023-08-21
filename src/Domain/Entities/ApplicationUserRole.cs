using Microsoft.AspNetCore.Identity;

namespace SiMinor7.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUserRole()
    {
    }
}