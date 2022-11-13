namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.UnitTests.Shows.Playlists;

public sealed class XmlDatav1SegmentParserTests
{
    [Theory]
    [MemberData(nameof(SegmentTestDataAsObjectArray))]
    public void Parses_timestring_to_timespan(
        string segmentString, Segment expectedSegment
    )
        => new XmlDatav1SegmentParser().Parse(segmentString)
            .Start.ShouldBe(expectedSegment.Start);

    [Theory]
    [MemberData(nameof(SegmentTestDataAsObjectArray))]
    public void Parses_to_expected_segment_type(
        string segmentString, Segment expectedSegment
    )
        => new XmlDatav1SegmentParser().Parse(segmentString)
            .Type.ShouldBe(expectedSegment.Type);

    [Theory]
    [MemberData(nameof(SongSegmentTestDataAsObjectArray))]
    public void Parses_to_artist_from_given_string(
        string segmentString, SongSegment expectedSegment
    )
    {
        var sut = new XmlDatav1SegmentParser().Parse(segmentString) as SongSegment;
        sut!.Artist.ShouldBe(expectedSegment.Artist);
    }

    [Theory]
    [MemberData(nameof(SongSegmentTestDataAsObjectArray))]
    public void Parses_to_song_from_given_string(
        string segmentString, SongSegment expectedSegment
    )
    {
        var sut = new XmlDatav1SegmentParser().Parse(segmentString) as SongSegment;
        sut!.Song.ShouldBe(expectedSegment.Song);
    }

    public static IEnumerable<object[]> SongSegmentTestDataAsObjectArray()
        => SegmentTestData()
            .Where(x => x.ExpectedSegment.Type == SegmentType.Song)
            .Select(ToObjectArray());

    public static IEnumerable<object[]> SegmentTestDataAsObjectArray()
        => SegmentTestData().Select(ToObjectArray());

    private static Func<(string SegmentString, Segment ExpectedSegment), object[]> ToObjectArray()
        => x => new object[] { x.SegmentString, x.ExpectedSegment };

    public static IEnumerable<(string SegmentString, Segment ExpectedSegment)> SegmentTestData()
        => new (string SegmentString, Segment ExpectedSegment)[]
        {
            (
                "00:00:00",
                JingleSegment.CreateJingle(TimeSpan.Zero, SegmentType.Jingle)
            ),
            (
                "00:00:00 Intro",
                JingleSegment.CreateIntro(TimeSpan.Zero, "Intro")
            ),
            (
                "00:00:00 START",
                JingleSegment.CreateIntro(TimeSpan.Zero, "START")
            ),
            (
                "00:00:43 Peter Elm - Black Sheep",
                SongSegment.CreateSong(new TimeSpan(0, 0, 43), "Peter Elm", "Black Sheep")
            ),
            (
                "00:21:35 X-RL7 ft. Nyxx - Under My Spell",
                SongSegment.CreateSong(new TimeSpan(0, 21, 35), "X-RL7 ft. Nyxx", "Under My Spell")
            ),
            (
                "00:55:16 Artist1-Artist2 - Song Name (club trance remix - radio edit)",
                SongSegment.CreateSong(new TimeSpan(0, 55, 16), "Artist1-Artist2", "Song Name (club trance remix - radio edit)")
            ),
            (
                "00:39:48 Rohn-Lederman - Watch Out (Assemblage 23 remix)",
                SongSegment.CreateSong(new TimeSpan(0, 39, 48), "Rohn-Lederman", "Watch Out (Assemblage 23 remix)")
            ),
            (
                "01:00:00 Outro",
                JingleSegment.CreateOutro(new TimeSpan(1, 0, 0), "Outro")
            ),
            (
                "01:00:00    eNd  ",
                JingleSegment.CreateOutro(new TimeSpan(1, 0, 0), "eNd")
            ),
        };
}
