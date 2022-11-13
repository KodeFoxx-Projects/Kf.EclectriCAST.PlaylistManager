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
        => Segments = segments ?? Enumerable.Empty<Segment>();

    /// <summary>
    /// Gets the segments of this <see cref="Playlist"/>.
    /// </summary>
    public IEnumerable<Segment> Segments { get; init; }

    /// <summary>
    /// Determines whether this <see cref="Playlist"/> has any <see cref="Segment"/>s.
    /// </summary>
    public bool HasSegments => Segments != null && Segments.Any();

    /// <inheritdoc />    
    protected override IEnumerable<object?> GetEqualityComponents()
        => new[] { Segments };
}