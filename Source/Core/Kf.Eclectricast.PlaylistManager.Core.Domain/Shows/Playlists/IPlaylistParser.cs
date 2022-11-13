namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists;

/// <summary>
/// Parses a playlist.
/// </summary>
public interface IPlaylistParser
{
    /// <summary>
    /// Parses a <see cref="String"/> to a <see cref="Playlist"/>
    /// </summary>
    /// <param name="playlist">The raw playlist data as <see cref="String"/>.</param>
    /// <returns>A <see cref="Playlist"/>.</returns>
    Playlist Parse(string playlist);
}