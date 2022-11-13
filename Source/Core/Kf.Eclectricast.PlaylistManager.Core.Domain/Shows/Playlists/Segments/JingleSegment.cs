namespace Kf.Eclectricast.PlaylistManager.Core.Domain.Shows.Playlists.Segments;

/// <summary>
/// Defines a jingle <see cref="Segment"/>.
/// </summary>
public sealed class JingleSegment : Segment
{
    /// <inheritdoc />    
    protected override IEnumerable<SegmentType> GetSupportedSegmentTypes()
        => new[] {
            SegmentType.Jingle,
            SegmentType.Intro,
            SegmentType.Outro
        };

    /// <summary>
    /// Represents an empty <see cref="JingleSegment"/>.
    /// </summary>
    public static readonly JingleSegment Empty = new();

    /// <summary>
    /// Creates a jingle segment.
    /// </summary>
    /// <param name="startTime">The start time of the segment.</param>
    /// <param name="title">The title of the segment, if any.</param>   
    public static Segment CreateJingle(TimeSpan startTime, SegmentType jingleType, string? title = null)
        => JingleSegment.Create(startTime, jingleType, title);

    /// <summary>
    /// Creates an intro jingle segment.
    /// </summary>
    /// <param name="startTime">The start time of the segment.</param>
    /// <param name="title">The title of the segment, if any.</param>   
    public static Segment CreateIntro(TimeSpan startTime, string? title = null)
        => CreateJingle(startTime, SegmentType.Intro, title);

    /// <summary>
    /// Creates an outro jingle segment.
    /// </summary>
    /// <param name="startTime">The start time of the segment.</param>
    /// <param name="title">The title of the segment, if any.</param>   
    public static Segment CreateOutro(TimeSpan startTime, string? title = null)
        => CreateJingle(startTime, SegmentType.Outro, title);

    /// <summary>
    /// Creates a <see cref="JingleSegment"/>.
    /// </summary>
    /// <param name="start">The start time of the segment.</param>
    /// <param name="jingleType">The type of jingle.</param>
    /// <param name="title">The text attached to the jingle, if any.</param>
    /// <exception cref="ArgumentException">When given <paramref name="jingleType"/> not supported.</exception>
    public static JingleSegment Create(TimeSpan startTime, SegmentType jingleType, string? text)
        => new(null, startTime, jingleType, text);

    /// <summary>
    /// Creates an empty <see cref="JingleSegment"/>
    /// </summary>
    private JingleSegment()
        : this(null, TimeSpan.MaxValue, SegmentType.Jingle, null)
    { }

    /// <summary>
    /// Creates a <see cref="JingleSegment"/>
    /// </summary>
    private JingleSegment(
        long? id,
        TimeSpan start,
        SegmentType type,
        string? text
    )
        : base(id, start, type)
        => Text = String.IsNullOrWhiteSpace(text) ? null : Text;

    /// <summary>
    /// Text of the jingle.
    /// </summary>
    public string? Text { get; init; }
}
