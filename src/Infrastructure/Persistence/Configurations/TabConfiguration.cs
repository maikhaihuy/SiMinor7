using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Infrastructure.Persistence.Configurations;

public class TabConfiguration : IEntityTypeConfiguration<Tab>
{
    public TabConfiguration()
    {
    }

    public void Configure(EntityTypeBuilder<Tab> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.Slug)
            .HasMaxLength(256);
        builder.Property(t => t.Thumbnail)
            .HasMaxLength(2000);
        builder.Property(t => t.Status)
            .IsRequired()
            .HasDefaultValue(TabStatus.Draft);

        builder.Property(t => t.SeoTitle)
            .HasMaxLength(255);
        builder.Property(t => t.SeoKeywords)
            .HasMaxLength(2000);
    }
}