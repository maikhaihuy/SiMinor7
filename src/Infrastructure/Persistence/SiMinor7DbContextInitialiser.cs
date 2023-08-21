using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence;

public class SiMinor7DbContextInitialiser
{
    private readonly ILogger<SiMinor7DbContextInitialiser> _logger;
    private readonly SiMinor7DbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SiMinor7DbContextInitialiser(ILogger<SiMinor7DbContextInitialiser> logger, SiMinor7DbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer() && !_context.Users.Any(u => u.Email == "admin@hminor7.xyz"))
            {
                await _context.Database.MigrateAsync();
                await SeedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new ApplicationRole(Application.Common.Constants.Role.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var moderatorRole = new ApplicationRole(Application.Common.Constants.Role.Moderator);

        if (_roleManager.Roles.All(r => r.Name != moderatorRole.Name))
        {
            await _roleManager.CreateAsync(moderatorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "admin@hminor7.xyz", Email = "admin@hminor7.xyz", Status = Domain.Enums.UserStatus.Actived, EmailConfirmed = true, DateCreated = DateTimeOffset.Now };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "123qweasdzxc");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
    }
}