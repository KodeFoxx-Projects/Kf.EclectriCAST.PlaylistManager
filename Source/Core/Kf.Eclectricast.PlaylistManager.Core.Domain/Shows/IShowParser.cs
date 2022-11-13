namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows;

/// <summary>
/// Parses a show.
/// </summary>
public interface IShowParser
{
    /// <summary>
    /// Parses a <see cref="String"/> to a <see cref="Show"/>.
    /// </summary>
    /// <param name="show">The raw show data as <see cref="String"/>.</param>
    /// <returns>A <see cref="Show"/>.</returns>
    Show Parse(string show);

    /// <summary>
    /// Parses a file to a <see cref="Show"/>.
    /// </summary>
    /// <param name="showDataFile">The file containing the data of the show.</param>
    /// <returns>A <see cref="Show"/>.</returns>
    Show Parse(FileInfo showDataFile);
}
