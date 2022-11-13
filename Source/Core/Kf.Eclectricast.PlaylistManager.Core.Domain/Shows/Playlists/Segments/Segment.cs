namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

/// <summary>
/// Represents a segment inside a <see cref="Playlist"/>.
/// </summary>
public abstract class Segment : Entity
{
    /// <summary>
    /// Creates an empty <see cref="Segment"/>.
    /// </summary>
    protected Segment()
        : this(
            id: null,
            start: TimeSpan.MaxValue,
            type: SegmentType.Empty
        )
    { }

    protected Segment(
        long? id,
        TimeSpan start,
        SegmentType type
    )
        : base(id)
    {
        Start = start;
        Type = Guard.Against.InvalidInput(
            type,
            nameof(type),
            input => GetSupportedSegmentTypes().Contains(input)
        );
    }

    /// <summary>
    /// Gets the supported <see cref="SegmentType"/>s.
    /// </summary>    
    protected abstract IEnumerable<SegmentType> GetSupportedSegmentTypes();

    /// <summary>
    /// Gets the starting time of the <see cref="Segment"/>.
    /// </summary>
    public TimeSpan Start { get; init; }

    /// <summary>
    /// Gets the type of <see cref="Segment"/>.
    /// </summary>
    public SegmentType Type { get; init; }
}
