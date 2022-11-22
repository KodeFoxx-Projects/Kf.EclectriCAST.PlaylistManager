namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

public sealed class EntityTests
{
    [Fact]
    public void Equality_is_based_on_id()
    {
        var nameA = Name.Create("Yves", "Schelpe");
        var nameB = Name.Create("John", "Doe");
        var personA = CreatePersonWith(id: 1, name: nameA);
        var personB = CreatePersonWith(id: 1, name: nameB);

        personA.Equals(personB).ShouldBeTrue();
        (personA == personB).ShouldBeTrue();
        (personA != personB).ShouldBeFalse();
    }

    [Fact]
    public void Negative_equality_is_based_on_id()
    {
        var name = Name.Create("Yves", "Schelpe");
        var personA = CreatePersonWith(id: 1, name);
        var personB = CreatePersonWith(id: 2, name);

        personA.Equals(personB).ShouldBeFalse();
        (personA == personB).ShouldBeFalse();
        (personA != personB).ShouldBeTrue();
    }

    [Fact]
    public void When_both_are_null_they_are_equal()
    {
        Person personA = null!;
        Person personB = null!;

        (personA == personB).ShouldBeTrue();
        (personA != personB).ShouldBeFalse();
        (personB == personA).ShouldBeTrue();
        (personB != personA).ShouldBeFalse();
    }

    [Fact]
    public void When_only_one_is_null_they_are_not_equal()
    {
        Person personA = null!;
        var personB = CreatePersonYvesSchelpe();

        (personA == personB).ShouldBeFalse();
        (personA != personB).ShouldBeTrue();
        (personB == personA).ShouldBeFalse();
        (personB != personA).ShouldBeTrue();
    }

    [Fact]
    public void HashCode_is_the_same_when_the_ids_are_the_same()
    {
        var id = 1;
        var personA = Person.Create(id, Name.Create("Yves", "Schelpe"));
        var personB = Person.Create(id, Name.Create("John", "Doe"));

        personA.GetHashCode().ShouldBe(personB.GetHashCode());
    }

    [Fact]
    public void ToString_returns_type_and_id_by_default()
    {
        var id = 1;
        var person = Person.Create(id, Name.Create("Yves", "Schelpe"));

        person.ToString().ShouldBe($"Person: {id}");
    }

    [Fact]
    public void Throws_ArgumentNullException_when_trying_to_pass_null_in_constructor()
    {
        Action act = () => new GuidEntity(null);

        act.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("id");
    }

    private static Person CreatePersonWith(long id = 1, Name name = null!)
        => Person.Create(id, name);
    private static Person CreatePersonYvesSchelpe(long id = 1)
        => CreatePersonWith(id, Name.Create("Yves", "Schelpe"));
}