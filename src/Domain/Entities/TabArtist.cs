using SiMinor7.Domain.Common;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class TabArtist : BaseAuditableEntity
{
    public int TabId { get; set; }
    public int ArtistId { get; set; }
    public TabSongType Type { get; set; }

    public Tab Tab { get; set; } = new();
    public Artist Artist { get; set; } = new();
}