using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Domain.Entities;
using SiMinor7.Infrastructure.Identity;
using SiMinor7.Infrastructure.Persistence.Interceptors;

namespace SiMinor7.Infrastructure.Persistence;

public class SiMinor7DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public SiMinor7DbContext(
        DbContextOptions<SiMinor7DbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Artist> Artists => Set<Artist>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<Song> Songs => Set<Song>();

    public DbSet<SongArtist> SongArtists => Set<SongArtist>();

    public DbSet<Tab> Tabs => Set<Tab>();

    public DbSet<TabArtist> TabArtists => Set<TabArtist>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}