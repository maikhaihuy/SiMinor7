using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(t => t.Avatar)
            .HasMaxLength(2000);
        builder.Property(t => t.FirstName)
            .HasMaxLength(50);
        builder.Property(t => t.LastName)
            .HasMaxLength(50);
    }
}