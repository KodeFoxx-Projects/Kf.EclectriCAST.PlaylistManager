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
    public static Show Create(ShowHeaderInfo showHeaderInfo)
    {
        var headerInfo = Guard.Against.Null(showHeaderInfo);
        return Create(name: headerInfo.File.Name, showHeaderInfo: headerInfo);
    }

    /// <summary>
    /// Creates a new <see cref="Show"/> with an empty <see cref="Playlist"/> and and empty <see cref="ShowHeaderInfo"/>.
    /// </summary>
    public static Show Create(string name, int? number = null, ShowHeaderInfo? showHeaderInfo = null)
        => new Show(id: null, header: showHeaderInfo, playlist: Playlist.Empty, name: name, number: number);

    /// <summary>
    /// Creates an empty <see cref="Show"/>.
    /// </summary>
    private Show()
        : base()
    {
        Header = ShowHeaderInfo.Empty;
        Playlist = Playlist.Empty;
        Name = String.Empty;
        Number = null;
    }

    /// <summary>
    /// Creates a new <see cref="Show"/>
    /// </summary>
    private Show(
        long? id,
        ShowHeaderInfo? header,
        Playlist playlist,
        string name,
        int? number
    )
        : base(id)
    {
        Header = header ?? ShowHeaderInfo.Empty;
        Playlist = Guard.Against.Null(playlist);
        Name = Guard.Against.NullOrWhiteSpace(name)
            .Trim()
            .ReplaceMultipleWhiteSpacesWithSingle();
        Number = Guard.Against.InvalidInput(number, nameof(number),
            n => n is null || n > 0, "Number has to be greater than '0'."
        );
    }

    /// <summary>
    /// Gets the header info of the show data file.
    /// </summary>
    public ShowHeaderInfo Header { get; init; }

    /// <summary>
    /// The playlist of the <see cref="Show"/>.
    /// </summary>
    public Playlist Playlist { get; private set; }

    /// <summary>
    /// Name of the <see cref="Show"/>.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Episode number of the <see cref="Show"/>, if any.
    /// </summary>
    public int? Number { get; private set; }

    /// <summary>
    /// Determines whether this show is part of a series of episodes.
    /// </summary>
    public bool IsEpisode => Number.HasValue;

    /// <summary>
    /// Replaces the current playlist by another one.
    /// </summary>
    public Show ReplacePlaylist(Playlist playlist)
    {
        Playlist = playlist;
        return this;
    }

    /// <summary>
    /// Changes the show's number
    /// </summary>
    public Show ChangeNumber(int? number)
    {
        Number = number;
        return this;
    }

    /// <summary>
    /// Changes the show's name
    /// </summary>
    public Show ChangeName(string name)
    {
        Name = name;
        return this;
    }
}
