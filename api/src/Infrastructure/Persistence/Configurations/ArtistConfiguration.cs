using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.Intro)
            .HasMaxLength(256);
        builder.Property(t => t.Avatar)
            .HasMaxLength(2000);
        builder.Property(t => t.Slug)
            .HasMaxLength(256);
        builder.Property(t => t.Status)
            .IsRequired();

        builder.Property(t => t.SeoTitle)
            .HasMaxLength(255);
        builder.Property(t => t.SeoKeywords)
            .HasMaxLength(2000);
    }
}