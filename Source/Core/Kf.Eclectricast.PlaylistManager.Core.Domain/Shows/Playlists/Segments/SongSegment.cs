namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

/// <summary>
/// Defines a song <see cref="Segment"/>.
/// </summary>
public sealed class SongSegment : Segment
{
    /// <inheritdoc />    
    protected override IEnumerable<SegmentType> GetSupportedSegmentTypes()
        => new[] {
            SegmentType.Song
        };

    /// <summary>
    /// Represents an empty <see cref="SongSegment"/>.
    /// </summary>
    public static readonly SongSegment Empty = new();

    /// <summary>
    /// Creates a <see cref="SongSegment"/>.
    /// </summary>
    /// <param name="startTime">The start time of the segment.</param>    
    /// <param name="songString">The string representing the artist.</param>
    /// <param name="songString">The string representing the song.</param>
    /// <exception cref="ArgumentException">When given <paramref name="jingleType"/> not supported.</exception>
    public static SongSegment CreateSong(TimeSpan startTime, string artistString, string songString)
        => new(null, startTime, artistString, songString);

    /// <summary>
    /// Creates an empty <see cref="SongSegment "/>
    /// </summary>
    private SongSegment()
        : base(null, TimeSpan.MaxValue, SegmentType.Song)
    {
        Artist = String.Empty;
        Song = String.Empty;
    }

    /// <summary>
    /// Creates a <see cref="SongSegment "/>
    /// </summary>
    private SongSegment(
        long? id,
        TimeSpan start,
        string? artistString,
        string? songString
    )
        : base(id, start, SegmentType.Song)
    {
        Artist = Guard.Against.NullOrWhiteSpace(artistString);
        Song = Guard.Against.NullOrWhiteSpace(songString);
    }

    /// <summary>
    /// Gets the artist.
    /// </summary>
    public string Artist { get; init; }

    /// <summary>
    /// Gets the song.
    /// </summary>
    public string Song { get; init; }
}