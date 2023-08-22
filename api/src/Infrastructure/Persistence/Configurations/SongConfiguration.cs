using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence.Configurations;

public class PerformanceConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.Property(t => t.KeyTone)
            .IsRequired();
        builder.Property(t => t.Url)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(t => t.TabId)
            .IsRequired();
    }
}