namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

/// <summary>
/// Represents a <see cref="Person"/>'s name.
/// </summary>
internal sealed class Name : ValueObject
{
    /// <summary>
    /// Represents an empty <see cref="Name"/>.
    /// </summary>
    public static readonly Name Empty = new Name();

    /// <summary>
    /// Creates a new <see cref="NamePart"/> from a string.
    /// </summary>
    /// <param name="firstName">The string acting as the value for <see cref="First"/> of this <see cref="NamePart"/>.</param>
    /// <param name="lastName">The string acting as the value for <see cref="Last"/> of this <see cref="NamePart"/>.</param>
    internal static Name Create(string firstName, string lastName)
        => new Name(firstName, lastName);

    /// <summary>
    /// Creates a new <see cref="Name"/> with a <paramref name="firstName"/> and <paramref name="lastName"/>.
    /// </summary>
    /// <param name="firstName">The <see cref="string"/> acting as the <paramref name="firstName"/>.</param>
    /// <param name="lastName">The <see cref="string"/> acting as the <paramref name="lastName"/>.</param>
    public Name(string firstName, string lastName)
        : this(firstName, lastName, null!)
    { }

    /// <summary>
    /// Creates a new <see cref="Name"/> with a <paramref name="firstName"/>, <paramref name="lastName"/>
    /// and <paramref name="middleNames"/>.
    /// </summary>
    /// <param name="firstName">The <see cref="string"/> acting as the <paramref name="firstName"/>.</param>
    /// <param name="lastName">The <see cref="string"/> acting as the <paramref name="lastName"/>.</param>
    /// <param name="middleNames">The <see cref="string[]"/> acting as the container for the <paramref name="middleNames"/>.</param>
    public Name(string firstName, string lastName, params string[] middleNames)
    {
        if (String.IsNullOrWhiteSpace(firstName))
            First = NamePart.Empty;
        else
            First = NamePart.Create(firstName);

        if (String.IsNullOrWhiteSpace(lastName))
            Last = NamePart.Empty;
        else
            Last = NamePart.Create(lastName);

        if (middleNames is null || middleNames.Length == 0)
            MiddleNames = Enumerable.Empty<NamePart>();
        else
            MiddleNames = middleNames.Select(mn => NamePart.Create(mn)).ToList();
    }

    /// <summary>
    /// Creates an empty <see cref="Name"/>.
    /// </summary>
    private Name() : this(null!, null!) { }

    /// <summary>
    /// First name.
    /// </summary>
    public NamePart First { get; init; }

    /// <summary>
    /// Middle names ordered by descending, if any.
    /// </summary>
    public IEnumerable<NamePart> MiddleNames { get; init; }

    /// <summary>
    /// Determines if any <see cref="MiddleNames"/> are present.
    /// </summary>
    public bool HasMiddleNames => MiddleNames != null && MiddleNames.Any();

    /// <summary>
    /// Surname (or last name).
    /// </summary>
    public NamePart Last { get; init; }

    /// <inheritdoc />
    protected override IEnumerable<object?> GetEqualityComponents()
        => new object?[] { First, MiddleNames, Last };
}
