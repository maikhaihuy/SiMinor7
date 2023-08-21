using SiMinor7.Domain.Common;

namespace SiMinor7.Domain.Entities;

public class Genre : BaseAuditableEntity
{
    public bool IsDeleted { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Total { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public string? Slug { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? SeoKeywords { get; set; }
}