namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

internal sealed class GuidEntity : Entity<Guid?>
{
    public GuidEntity(Guid? id) : base(id) { }
}