namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

internal sealed class Person : Entity<long>
{
    public static readonly Person Empty = new Person();

    public static Person Create(long id, Name name)
        => new Person(id, name);
    public static Person Create(long id, string firstName, string lastName)
        => new Person(id, Name.Create(firstName, lastName));

    private Person(long id, Name name)
        : base(id)
        => Name = name;
    private Person()
        : this(0, Name.Empty)
    { }

    public Name Name { get; }

    public Person Rename(Name name)
        => new Person(Id, name);
}
