using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Infrastructure.Persistence.Configurations;
internal class SessionLoginConfiguration : IEntityTypeConfiguration<SessionLogin>
{
    public void Configure(EntityTypeBuilder<SessionLogin> builder)
    {
        builder.Property(t => t.RefreshToken)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(t => t.UserAgent)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(t => t.UserId)
            .HasMaxLength(450)
            .IsRequired();
    }
}