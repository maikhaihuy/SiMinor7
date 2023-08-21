using SiMinor7.Domain.Common;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class Tab : BaseAuditableEntity
{
    public bool IsDeleted { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public TabStatus Status { get; set; }
    public ulong? Views { get; set; }
    public uint? Rating { get; set; }
    public KeyTone KeyTone { get; set; }
    public int GenreId { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? SeoKeywords { get; set; }

    public Genre Gerne { get; set; } = null!;
    public ICollection<TabArtist> TabArtists = new List<TabArtist>();
}