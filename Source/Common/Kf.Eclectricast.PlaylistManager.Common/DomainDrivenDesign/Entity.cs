using Ardalis.GuardClauses;

namespace Kf.Eclectricast.PlaylistManager.Common.DomainDrivenDesign;

/// <summary>
/// Represents an entity.
/// </summary>
public abstract class Entity<TId>
    : IEquatable<Entity<TId>>
{
    /// <summary>
    /// Creates a new Entity.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="id"/> is null.</exception>
    protected Entity(TId id)
        => Id = Guard.Against.Null(id, nameof(id));

    /// <summary>
    /// Identifier of the entity.
    /// </summary>
    public TId Id { get; }

    /// <inheritdoc/>
    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other is Entity<TId> entity) return Equals(entity);
        return false;
    }

    /// <inheritdoc cref="IEquatable{T}.Equals(T?)"/>
    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return CompareId(Id, other.Id);
    }

    /// <summary>
    /// Compares two <typeparamref name="TId"/>'s and returns true when they match, false when not.
    /// When both a and b are null, they're considered not equal.
    /// Override when using custom id's, or when applying special logic (e.g. when "0" doesn't matter).
    /// </summary>
    protected virtual bool CompareId(TId a, TId b)
    {
        if (a is null && b is null) return true;

        if (a is TId idA && b is TId idB)
            return idA.Equals(idB);

        return false;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => GetIdentityString().GetHashCode();

    /// <inheritdoc/>
    public override string ToString()
        => GetIdentityString();

    /// <summary>
    /// Generates an identity string for debugging.
    /// </summary>
    private string GetIdentityString()
    {
        var identityStringTypePart = $"{GetType().Name}";

        if (Id is null)
            return $"{identityStringTypePart}: null";

        return $"{identityStringTypePart}: {Id}";
    }

    /// <inheritdoc/>
    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    /// <inheritdoc/>
    public static bool operator !=(Entity<TId> a, Entity<TId> b)
        => !(a == b);
}
