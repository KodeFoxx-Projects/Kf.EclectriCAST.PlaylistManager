namespace Kf.Eclectricast.PlaylistManager.Core.Domain;

/// <summary>
/// Defines the base entity for this domain.
/// </summary>
public abstract class Entity : Entity<long>
{
    /// <inheritdoc />    
    protected Entity(long? id)
        : base(id ?? 0)
    { }

    /// <summary>
    /// Creates an empty <see cref="Entity{TId}"/>.
    /// </summary>
    protected Entity()
        : this(null)
    { }
}
