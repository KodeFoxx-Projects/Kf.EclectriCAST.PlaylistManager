namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

internal sealed class ValueObjectWithNullValues : ValueObject
{
    /// <summary>
    /// The string value of the name part.
    /// </summary>
    public string StringValue { get => null!; }

    /// <summary>
    /// The string value of the name part.
    /// </summary>
    public int? Int32Value { get => null!; }

    /// <inheritdoc />    
    protected override IEnumerable<object?> GetEqualityComponents()
        => new object?[] { StringValue, Int32Value };
}