using SiMinor7.Domain.Common;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class Song : BaseAuditableEntity
{
    public bool IsDeleted { get; set; }

    public string? Description { get; set; }
    public KeyTone KeyTone { get; set; }
    public string Url { get; set; } = string.Empty;
    public int TabId { get; set; }

    public Tab Tab { get; set; } = null!;
    public ICollection<SongArtist> SongArtists { get; set; } = new List<SongArtist>();
}