namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Tests.Shows.Playlists;

public sealed class XmlDatav1PlaylistParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("         ")]
    public void Empty_or_null_string_returns_empty_playlist(string playlist)
    {
        var sut = new XmlDatav1PlaylistParser(new XmlDatav1SegmentParser())
            .Parse(playlist);

        sut.ShouldNotBeNull();
        sut.Segments.ShouldNotBeNull();
        sut.Segments.ShouldBeEmpty();
        sut.HasSegments.ShouldBeFalse();
        sut.ShouldBe(Playlist.Empty);
    }

    [Theory]
    [MemberData(nameof(PlaylistTestDataAsObjectData))]
    public void Parses_full_string_to_playlist(
        string playlist, int expectedNumberOfSegments
    )
    {
        var sut = new XmlDatav1PlaylistParser(new XmlDatav1SegmentParser())
            .Parse(playlist);

        sut.ShouldNotBeNull();
        sut.Segments.ShouldNotBeEmpty();
        sut.HasSegments.ShouldBeTrue();
        sut.Segments.Count().ShouldBe(expectedNumberOfSegments);
    }

    [Theory]
    [MemberData(nameof(PlaylistTestDataAsObjectData))]
    public void Playlist_orders_segments_by_Start(
        string playlist, int expectedNumberOfSegments
    )
    {
        var sut = new XmlDatav1PlaylistParser(new XmlDatav1SegmentParser())
            .Parse(playlist);

        sut.Segments.First().Type.ShouldBe(SegmentType.Intro);
        sut.Segments.Last().Type.ShouldBe(SegmentType.Outro);

        TimeSpan? previousTimeSpan = null;
        foreach (var segment in sut.Segments)
        {
            if (previousTimeSpan != null)
                segment.Start.ShouldBeGreaterThan(previousTimeSpan.Value);

            previousTimeSpan = segment.Start;
        }
    }

    public static IEnumerable<object[]> PlaylistTestDataAsObjectData()
        => PlaylistTestData()
            .Select(x => new object[] {
                x,
                x.Split(Environment.NewLine).Count()
            }
        );

    public static IEnumerable<string> PlaylistTestData()
        => new[]
        {
            """
            00:00:00 Intro
            00:00:43 Peter Elm - Black Sheep
            00:04:00 Charlotte Adigéry & Bolis Pupul - Ceci N'Est Pas Un Cliché
            00:07:28 Dana Jean Phoenix & Powernerd - Moves Moves Moves
            00:11:00 Spray - Hammered In An Airport
            00:13:39 Sierra - Gone (NEUS remix)
            00:11:02 Sally Dige - Be Gone
            00:15:18 Selah Sue - You (S+C+A+R+R remix)
            00:17:59 Junksista ft. Emke (Black Nail Cabaret) - Do You Wanna
            00:21:35 X-RL7 ft. Nyxx - Under My Spell
            00:25:12 Sick Jokes - This Is The Beginning
            00:29:05 Lords Of Acid - Feed My Hungry Soul
            00:32:11 Extra Credit - It's Over
            00:36:05 Movulango - Other Way
            00:38:44 Sally Dige - Be Gone
            00:42:58 Purple Fog Side & Elsehow - End Of Summer
            00:47:45 Psy'Aviah ft. Marieke Lightband - My Secrets (Entrzelle 7" remix)
            00:52:45 diskonnekted - Yesteryears (club version)
            00:56:02 Mari Kattman - Fever Shakes
            01:00:00 End
            """,
            """
            00:00:00 Intro
            00:00:43 Mecha Maiko - Fade To Black
            00:02:20 Dana Jean Phoenix - Only For One Night
            00:05:35 She Hates Emotions - This Ain't Good Master
            00:08:42 Max M - Never Wanna Leave (Klaas Remix)
            00:10:53 Milk Inc - Sunrise
            00:13:53 Nena & Kim Wilde - Anyplace, Anywhere, Anytime
            00:17:16 Humans Can't Reboot - Ghosts
            00:21:11 Moby - Rescue Me
            00:24:23 Mildreda - Blame It On The Moon
            00:29:28 Billie Eilish - NDA
            00:32:58 DAAN - Love
            00:36:40 Dynalectric Orchestra - When Logic Collides With Emotion
            00:39:48 Rohn-Lederman - Watch Out (Assemblage 23 remix)
            00:43:39 Miss FD - Adore
            00:46:07 [aesthetische] - Racing Backwards
            00:49:31 Ayria - The Gun Song
            00:52:48 ELM - Excuses Excuses
            00:55:06 Entrzelle - Everyday Criminal (Simon Carter remix)
            00:59:57 End
            """
        };
}