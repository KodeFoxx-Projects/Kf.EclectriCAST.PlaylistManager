namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

/// <summary>
/// Type of <see cref="Segment"/>.
/// </summary>
public enum SegmentType
{
    Undefined = 0,
    Empty = 100_000_000,
    Talk = 200_000_000,
    Music = 300_000_000,
    Song = 310_000_000,
    Jingle = 400_000_000,
    Intro = 410_000_000,
    Outro = 420_000_000
}
