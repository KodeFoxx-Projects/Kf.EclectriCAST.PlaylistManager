namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows;

/// <summary>
/// Parses a show.
/// </summary>
public interface IShowParser
{
    /// <summary>
    /// Parses a file to a <see cref="Show"/>.
    /// </summary>
    /// <param name="showDataFile">The file containing the data of the show.</param>
    /// <returns>A <see cref="Show"/>.</returns>
    Show Parse(FileInfo showDataFile);
}
