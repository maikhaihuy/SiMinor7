using SiMinor7.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SiMinor7.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }

    DbSet<ApplicationRole> Roles { get; }

    DbSet<ApplicationUserRole> UserRoles { get; }

    DbSet<Artist> Artists { get; }

    DbSet<Genre> Genres { get; }

    DbSet<Song> Songs { get; }

    DbSet<SongArtist> SongArtists { get; }

    DbSet<Tab> Tabs { get; }

    DbSet<TabArtist> TabArtists { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}