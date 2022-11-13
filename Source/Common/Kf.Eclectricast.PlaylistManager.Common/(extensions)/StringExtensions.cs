using System.Text.RegularExpressions;

namespace Kf.Eclectricast.PlaylistManager;

/// <summary>
/// Extension methods for <see cref="String"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Cache for <see cref="ReplaceMultipleWhiteSpacesWithSingle(string, bool)"/> compiled regex.
    /// </summary>
    private static Regex? _multipleWhiteSpacesRegex;

    /// <summary>
    /// Removes multiple occurences of white spaces in a string.
    /// </summary>
    /// <param name="string">The string to remove multiple occurences from.</param>
    /// <param name="string">The string to replace the occurences with.</param>
    /// <returns>A <see cref="String"/> without the multiple white space occurences.</returns>
    public static String ReplaceMultipleWhiteSpacesWithSingle(
        this string @string, bool removeTabs = true
    )
    {
        if (String.IsNullOrEmpty(@string)) return String.Empty;

        if (_multipleWhiteSpacesRegex is null)
            _multipleWhiteSpacesRegex = new Regex(
                pattern: "[  ]{2,}",
                options: RegexOptions.None | RegexOptions.Compiled
            );

        var cleanedString = removeTabs ? @string.RemoveTabs() : @string;
        cleanedString = cleanedString.Replace("  ", " ");
        cleanedString = _multipleWhiteSpacesRegex.Replace(cleanedString, " ");


        return cleanedString;
    }

    /// <summary>
    /// Cache for <see cref="RemoveTabs(string)/> compiled regex.
    /// </summary>
    private static Regex? _tabRegex;

    /// <summary>
    /// Removes tabs by replacing them with an empty string.
    /// </summary>
    /// <param name="string">The string to remove tabs from.</param>
    /// <returns>A <see cref="String"/> without tabs.</returns>
    public static String RemoveTabs(this string @string)
    {
        if (String.IsNullOrEmpty(@string)) return String.Empty;

        if (_tabRegex is null)
            _tabRegex = new Regex(
                pattern: "[\t]",
                options: RegexOptions.None | RegexOptions.Compiled
            );

        return _tabRegex.Replace(@string, "");
    }
}

