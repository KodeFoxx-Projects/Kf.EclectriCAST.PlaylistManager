namespace Kf.Eclectricast.PlaylistManager.Common.DomainDrivenDesign;

public abstract class ValueObject
    : IEquatable<ValueObject>
{
    /// <inheritdoc />
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    /// <inheritdoc />
    public static bool operator !=(ValueObject a, ValueObject b)
        => !(a == b);

    /// <summary>
    /// Enumerable of objects in the <see cref="ValueObject"/> that define its identity.
    /// </summary>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? @object)
    {
        if (@object == null) return false;
        if (@object.GetType() != GetType()) return false;

        if (
            @object is ValueObject other
            && (other != null || !ReferenceEquals(other, null))
        )
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

        return false;
    }

    /// <inheritdoc cref="IEquatable{T}.Equals(T?)"/>
    public bool Equals(ValueObject? other)
        => Equals(this, other);

    /// <inheritdoc />
    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);

    /// <inheritdoc />
    public override string ToString()
    {
        var equalityComponents = GetEqualityComponents()
            .Where(x => x != null)
            .Select(x => $"\"{x}\"");

        var equalityComponentsString = equalityComponents.Any()
            ? String.Join(", ", equalityComponents)
            : "(*EMPTY)";

        return $"{GetType().Name}: {{ {equalityComponentsString} }}";
    }
}
