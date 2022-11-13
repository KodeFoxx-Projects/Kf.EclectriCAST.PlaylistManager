using Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists;

/// <summary>
/// Parses a segment.
/// </summary>
public interface ISegmentParser
{
    /// <summary>
    /// Parses a <see cref="String"/> to a <see cref="Segment"/>
    /// </summary>
    /// <param name="segment">The raw segment data as <see cref="String"/>.</param>
    /// <returns>A <see cref="Segment"/>.</returns>
    Segment Parse(string segment);
}