using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.Total)
            .HasDefaultValue(0);
        builder.Property(t => t.Thumbnail)
            .HasMaxLength(2000);
        builder.Property(t => t.Slug)
            .HasMaxLength(256);

        builder.Property(t => t.SeoTitle)
            .HasMaxLength(255);
        builder.Property(t => t.SeoKeywords)
            .HasMaxLength(2000);
    }
}