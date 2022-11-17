using Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists;

/// <summary>
/// Represents a <see cref="Show"/>'s playlist.
/// </summary>
public sealed class Playlist : ValueObject
{
    /// <summary>
    /// Represents an empty <see cref="Playlist"/>.
    /// </summary>
    public static readonly Playlist Empty = new();

    /// <summary>
    /// Creates a <see cref="Playlist"/>.
    /// </summary>
    public static Playlist Create()
        => new();

    /// <summary>
    /// Creates an empty <see cref="Playlist"/>.
    /// </summary>
    private Playlist()
        : this(null)
    { }

    /// <summary>
    /// Creates a
    /// </summary>
    /// <param name="segments"></param>
    private Playlist(IEnumerable<Segment>? segments)
        : base()
        => _segments = segments?.ToList() ?? new List<Segment>();

    /// <summary>
    /// Gets the segments of this <see cref="Playlist"/>.
    /// </summary>
    public IEnumerable<Segment> Segments => (
        _segments == null
            ? Enumerable.Empty<Segment>().ToList().AsReadOnly()
            : _segments.AsReadOnly()
        )
        .OrderBy(s => s.Start);

    private List<Segment> _segments = new List<Segment>();

    /// <summary>
    /// Determines whether this <see cref="Playlist"/> has any <see cref="Segment"/>s.
    /// </summary>
    public bool HasSegments => Segments != null && Segments.Any();

    /// <summary>
    /// Adds a <see cref="Segment"/> to the playlist.
    /// </summary>
    public Playlist AddSegment(Segment segment)
    {
        _segments.Add(segment);
        return this;
    }

    public Playlist AddSegments(IEnumerable<Segment> segments)
    {
        _segments.AddRange(segments);
        return this;
    }

    /// <inheritdoc />
    protected override IEnumerable<object?> GetEqualityComponents()
        => new[] { Segments };
}