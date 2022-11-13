namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

/// <summary>
/// Represents part of a <see cref="Name"/>
/// </summary>
internal sealed class NamePart : ValueObject
{
    /// <summary>
    /// Represents an empty <see cref="NamePart"/>.
    /// </summary>
    public static readonly NamePart Empty = new NamePart();

    /// <summary>
    /// Creates a new <see cref="NamePart"/> from a string.
    /// </summary>
    /// <param name="string">The string acting as the value for this <see cref="NamePart"/>.</param>
    internal static NamePart Create(string @string)
        => new NamePart(@string);

    /// <summary>
    /// Creates a new <see cref="NamePart"/> with a given <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> acting as the <paramref name="value"/>.</param>
    public NamePart(string value) => Value = value ?? String.Empty;

    /// <summary>
    /// Creates an empty <see cref="NamePart"/>.
    /// </summary>
    private NamePart() : this(String.Empty) { }

    /// <summary>
    /// The string value of the name part.
    /// </summary>
    public string Value { get; init; }

    /// <inheritdoc />
    protected override IEnumerable<object?> GetEqualityComponents()
        => new[] { Value };
}