using SiMinor7.Domain.Common;
using SiMinor7.Domain.Enums;

namespace SiMinor7.Domain.Entities;

public class SongArtist : BaseAuditableEntity
{
    public int SongId { get; set; }
    public int ArtistId { get; set; }
    public SongArtistType Type { get; set; }

    public Song Song { get; set; } = new();
    public Artist Artist { get; set; } = new();
}