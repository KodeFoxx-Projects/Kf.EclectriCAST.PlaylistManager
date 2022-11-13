namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows;

/// <summary>
/// Defines a show.
/// </summary>
public sealed class Show : Entity
{
    /// <summary>
    /// Represents an empty <see cref="Show"/>.
    /// </summary>
    public static readonly Show Empty = new();

    /// <summary>
    /// Creates a new <see cref="Show"/> with an empty <see cref="Playlist"/>.
    /// </summary>
    public static Show Create(string name, int? number = null)
        => new Show(id: null, playlist: Playlist.Empty, name: name, number: number);

    /// <summary>
    /// Creates an empty <see cref="Show"/>.
    /// </summary>
    private Show()
        : base()
    {
        Playlist = Playlist.Empty;
        Name = String.Empty;
        Number = null;
    }

    private Show(
        long? id,
        Playlist playlist,
        string name,
        int? number
    )
        : base(id)
    {
        Playlist = Guard.Against.Null(playlist);
        Name = Guard.Against.NullOrWhiteSpace(name)
            .Trim()
            .ReplaceMultipleWhiteSpacesWithSingle();
        Number = Guard.Against.InvalidInput(number, nameof(number),
            n => n is null || n > 0, "Number has to be greater than '0'."
        );
    }

    /// <summary>
    /// The playlist of the <see cref="Show"/>.
    /// </summary>
    public Playlist Playlist { get; init; }

    /// <summary>
    /// Name of the <see cref="Show"/>.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Episode number of the <see cref="Show"/>, if any.
    /// </summary>
    public int? Number { get; init; }

    /// <summary>
    /// Determines whether this show is part of a series of episodes.
    /// </summary>
    public bool IsEpisode => Number.HasValue;
}
