namespace Kf.Eclectricast.PlaylistManager.Core.UnitTests.Domain.Shows;

public sealed class ShowTests
{
    [Fact]
    public void Empty_show_has_empty_Playlist()
        => Show.Empty.Playlist.ShouldBe(Playlist.Empty);

    [Fact]
    public void Empty_show_has_empty_Name()
        => Show.Empty.Name.ShouldBe(String.Empty);

    [Fact]
    public void Empty_show_has_null_as_Number()
        => Show.Empty.Number.ShouldBeNull();

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Number_has_to_be_greater_than_0(int testNumber)
    {
        var exception = Should.Throw<ArgumentException>(
            () => Show.Create("The EclectriCAST", testNumber)
        );

        exception.ParamName.ShouldBe("number");
        exception.Message.ShouldContain("has to be greater than '0'");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(null!)]
    public void Number_has_to_be_higher_than_0_or_null(int? testNumber)
    {
        var sut = Show.Create("The EclectriCAST", testNumber);

        if (testNumber.HasValue)
            sut.Number.ShouldBe(testNumber);
        else
            sut.Number.ShouldBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Name_can_not_be_null_whitespace_or_empty(string? testName)
    {
        var exception = Should.Throw<ArgumentException>(
            () => Show.Create(testName!)
        );

        exception.ParamName.ShouldBe("name");
    }

    [Theory]
    [InlineData("The EclectriCAST", "The EclectriCAST")]
    [InlineData("  The EclectriCAST", "The EclectriCAST")]
    [InlineData("  The      EclectriCAST", "The EclectriCAST")]
    [InlineData("The EclectriCAST  ", "The EclectriCAST")]
    [InlineData("  The EclectriCAST  ", "The EclectriCAST")]
    [InlineData("  The   EclectriCAST  ", "The EclectriCAST")]
    public void Name_is_trimmed_of_double_leading_and_trailing_whitespace(
        string testName, string expected
    )
        => Show.Create(testName)
            .Name.ShouldBe(expected);


    [Fact]
    public void IsEpisode_should_return_false_when_number_is_null()
        => Show.Create("The EclectriCAST")
            .IsEpisode.ShouldBeFalse();

    [Fact]
    public void IsEpisode_should_return_false_when_number_is_present()
        => Show.Create("The EclectriCAST", 11)
            .IsEpisode.ShouldBeTrue();
}