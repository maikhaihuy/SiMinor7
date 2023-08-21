using SiMinor7.Domain.Common;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class Artist : BaseAuditableEntity
{
    public bool IsDeleted { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Intro { get; set; }
    public string? Avatar { get; set; }
    public string? Slug { get; set; }
    public ArtistStatus Status { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? SeoKeywords { get; set; }
}