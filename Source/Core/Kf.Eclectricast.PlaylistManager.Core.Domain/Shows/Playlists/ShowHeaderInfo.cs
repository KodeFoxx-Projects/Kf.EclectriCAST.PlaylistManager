namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists;

/// <summary>
/// Header info for the show data file.
/// </summary>
public class ShowHeaderInfo : ValueObject
{
    /// <summary>
    /// Represents an empty <see cref="ShowHeaderInfo"/>.
    /// </summary>
    public static readonly ShowHeaderInfo Empty = new();

    public static ShowHeaderInfo Create(string format, int version, FileInfo file)
        => new(format, version, file);

    /// <summary>
    /// Creates an empty <see cref="ShowHeaderInfo"/>.
    /// </summary>
    public ShowHeaderInfo()
        : base()
    {
        Format = String.Empty;
        Version = 0;
        File = null!;
    }

    /// <summary>
    /// Creates a <see cref="ShowHeaderInfo"/>.
    /// </summary>
    public ShowHeaderInfo(
        string format,
        int version,
        FileInfo file
    )
        : base()
    {
        Format = Guard.Against.NullOrWhiteSpace(format);
        Version = version;
        File = Guard.Against.Null(file);
    }

    /// <summary>
    /// Format of the data file.
    /// </summary>
    public string Format { get; init; }

    /// <summary>
    /// Version of the data file.
    /// </summary>
    public int Version { get; init; }

    /// <summary>
    /// Gets the file of the show data file.
    /// </summary>
    public FileInfo File { get; init; }

    /// <inheritdoc />    
    protected override IEnumerable<object?> GetEqualityComponents()
        => new object[] { Format, Version };
}
