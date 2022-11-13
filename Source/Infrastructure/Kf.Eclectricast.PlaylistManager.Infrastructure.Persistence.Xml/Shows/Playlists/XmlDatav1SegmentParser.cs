using Ardalis.GuardClauses;
using Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;
using System.Text.RegularExpressions;

namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Shows.Playlists;

/// <summary>
/// Parses a segment from a raw string, following a specific format: "<![CDATA[00:07:28 Artist - Title]]>";
/// Yet for other segments such as intro or outro it could look lie this: "<![CDATA[00:00:00 Intro]]>".
/// The parsing logic decides which kind of segment it is, and if it can't determine this it will
/// eventually throw an exception showing which line it can't parse.
/// </summary>
public sealed class XmlDatav1SegmentParser : ISegmentParser
{
    /// <inheritdoc />
    public Segment Parse(string segment)
    {
        Guard.Against.NullOrWhiteSpace(segment);

        var cleanedSegment = segment
            .Trim()
            .ReplaceMultipleWhiteSpacesWithSingle();

        var startTime = TryParseStartTime(cleanedSegment);
        string remainder = GetRemainder(cleanedSegment, startTime);

        if (
            String.IsNullOrWhiteSpace(remainder)
            || ArtistSongSplitterRegex.Count(remainder) == 0
        )
        {
            return remainder?.ToLower() switch
            {

                "outro" or "end" => JingleSegment.CreateOutro(startTime.TimeSpan, remainder),
                "intro" or "start" => JingleSegment.CreateIntro(startTime.TimeSpan, remainder),
                _ => JingleSegment.CreateJingle(startTime.TimeSpan, SegmentType.Jingle, remainder)
            };
        }


        var song = TryParseSongParts(remainder);

        return SongSegment.CreateSong(startTime.TimeSpan, song.Artist, song.Song);
    }

    private readonly static Regex TimeSpanRegex
        = new Regex(
            "(2[0-3]|[01][0-9]):([0-5][0-9]):([0-5][0-9])",
            RegexOptions.Compiled
        );

    private readonly static Regex ArtistSongSplitterRegex
        = new Regex(
            "( - )",
            RegexOptions.Compiled
        );

    private (TimeSpan TimeSpan, string String) TryParseStartTime(string segment)
    {
        Guard.Against.InvalidInput(segment, nameof(segment),
            input => input?.Trim().Length >= 8,
            "Could not parse time based string as it was less than 8 characters. Required format is 'hh:mm:ss'."
        );

        var timeString = segment.Substring(0, 8).Trim();

        Guard.Against.InvalidInput(segment, nameof(segment),
            input => TimeSpanRegex.IsMatch(input),
            $"Could not parse time based string: {timeString}. Required format is 'hh:mm:ss'."
        );

        return (
            TimeSpan: TimeSpan.Parse(timeString),
            String: timeString
        );
    }

    private string GetRemainder(string cleanedSegment, (TimeSpan TimeSpan, string String) startTime)
        => cleanedSegment
            .Substring(startTime.String.Length, cleanedSegment.Length - startTime.String.Length)
            .Trim();

    private (string Artist, string Song) TryParseSongParts(string remainder)
        => (
            Artist: TryParseArtist(remainder),
            Song: TryParseSong(remainder)
        );

    private string TryParseArtist(string remainder)
    {
        Guard.Against.InvalidInput(remainder, nameof(remainder),
            input => ArtistSongSplitterRegex.Count(remainder) >= 1,
            $"Multiple occurences of ' - ', could not identity which part is artist or song."
        );

        var splitted = ArtistSongSplitterRegex.Split(remainder);

        return splitted[0];
    }

    private string TryParseSong(string remainder)
    {
        Guard.Against.InvalidInput(remainder, nameof(remainder),
            input => ArtistSongSplitterRegex.Count(remainder) >= 1,
            $"Multiple occurences of ' - ', could not identity which part is artist or song."
        );

        var splitted = ArtistSongSplitterRegex.Split(remainder);

        return String.Join("", splitted[2..]);
    }
}
