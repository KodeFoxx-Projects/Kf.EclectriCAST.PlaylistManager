namespace Kf.Eclectricast.PlaylistManager.Common.UnitTests;

public sealed class StringExtensionTests
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("  ", " ")]
    [InlineData("Is this  removing   all   spaces     properly?", "Is this removing all spaces properly?")]
    public void ReplaceMultipleWhiteSpacesWithSingle_removes_multiple_occurences_with_single_space(
        string testString, string expected
    )
        => testString.ReplaceMultipleWhiteSpacesWithSingle()
            .ShouldBe(expected);

    [Theory]
    [InlineData(null, "")]
    [InlineData("\t \t \t", " ")]
    [InlineData("\t\t\t", "")]
    [InlineData(
        "Here comes a tab:\t. Did you remove it?",
        "Here comes a tab:. Did you remove it?")]
    [InlineData(
        "Tabs\t,   with   spaces      did you \t\t remove \t\t them?",
        "Tabs, with spaces did you remove them?")]
    public void ReplaceMultipleWhiteSpacesWithSingle_removes_tabs(
        string testString, string expected
    )
        => testString.ReplaceMultipleWhiteSpacesWithSingle()
            .ShouldBe(expected);

    [Theory]
    [InlineData(null, "")]
    [InlineData("\t\t\t", "")]
    [InlineData("\t \t \t", "  ")]
    [InlineData("Id\tName\t\r\n15\tJohn Doe", "IdName\r\n15John Doe")]
    public void RemoveTabs_removes_tabs(
        string testString, string expected
    )
        => testString.RemoveTabs()
            .ShouldBe(expected);
}
