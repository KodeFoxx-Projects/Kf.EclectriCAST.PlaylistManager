namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests.DomainDrivenDesign;

public sealed class ValueObjectTests
{
    [Fact]
    public void Equality_is_based_on_properties()
    {
        var firstName = "Yves";
        var lastName = "Schelpe";
        var nameA = Name.Create(firstName, lastName);
        var nameB = Name.Create(firstName, lastName);

        nameA.Equals(nameB).ShouldBeTrue();
        (nameA == nameB).ShouldBeTrue();
        (nameA != nameB).ShouldBeFalse();
    }

    [Fact]
    public void Negative_equality_is_based_on_properties()
    {
        var firstName = "Yves";
        var lastName = "Schelpe";
        var nameA = Name.Create(firstName, lastName);
        var nameB = Name.Create(firstName.ToLower(), lastName);

        nameA.Equals(nameB).ShouldBeFalse();
        (nameA == nameB).ShouldBeFalse();
        (nameA != nameB).ShouldBeTrue();
    }

    [Fact]
    public void When_both_are_null_they_are_equal()
    {
        Name nameA = null!;
        Name nameB = null!;

        (nameA == nameB).ShouldBeTrue();
        (nameA != nameB).ShouldBeFalse();
        (nameB == nameA).ShouldBeTrue();
        (nameB != nameA).ShouldBeFalse();
    }

    [Fact]
    public void When_only_one_is_null_they_are_not_equal()
    {
        Name nameA = null!;
        var nameB = Name.Create("Yves", "Schelpe");

        (nameA == nameB).ShouldBeFalse();
        (nameA != nameB).ShouldBeTrue();
        (nameB == nameA).ShouldBeFalse();
        (nameB != nameA).ShouldBeTrue();
    }

    [Fact]
    public void HashCode_is_based_on_properties()
    {
        var firstName = "Yves";
        var lastName = "Schelpe";
        var nameA = Name.Create(firstName, lastName);
        var nameB = Name.Create(firstName, lastName);

        nameA.GetHashCode().ShouldBe(nameB.GetHashCode());
    }

    [Fact]
    public void ToString_returns_type_and_equality_components_with_their_value_as_string()
    {
        var sut = new NamePart("John");
        sut.ToString().ShouldBe($"{nameof(NamePart)}: {{ \"John\" }}");
    }

    [Fact]
    public void ToString_ignores_null_values_in_the_equality_components()
    {
        var sut = new ValueObjectWithNullValues();
        sut.ToString().ShouldBe($"{nameof(ValueObjectWithNullValues)}: {{ {Literals.Empty} }}");
    }
}
