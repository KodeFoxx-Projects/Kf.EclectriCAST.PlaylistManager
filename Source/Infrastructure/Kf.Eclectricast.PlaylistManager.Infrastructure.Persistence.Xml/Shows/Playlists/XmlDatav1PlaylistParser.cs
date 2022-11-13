using Ardalis.GuardClauses;

namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Shows.Playlists;

/// <summary>
/// Parses a playlist from a raw string.
/// </summary>
public sealed class XmlDatav1PlaylistParser : IPlaylistParser
{
    private readonly ISegmentParser _segmentParser;

    /// <summary>
    /// Creates a new <see cref="XmlDatav1SegmentParser"/>.
    /// </summary>    
    public XmlDatav1PlaylistParser(ISegmentParser segmentParser)
        => _segmentParser = Guard.Against.Null(segmentParser);

    /// <inheritdoc />   
    public Playlist Parse(string playlistString)
    {
        if (String.IsNullOrWhiteSpace(playlistString))
            return Playlist.Empty;

        var playlistLines = playlistString.Split(Environment.NewLine);
        var playlist = Playlist.Create();
        foreach (var playlistLine in playlistLines)
        {
            var segment = _segmentParser.Parse(playlistLine);
            playlist.AddSegment(segment);
        }

        return playlist;
    }
}
