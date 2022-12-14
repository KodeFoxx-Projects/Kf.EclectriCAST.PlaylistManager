using System.Xml;

namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Shows;

/// <summary>
/// Parses an XML Show v1 file.
/// </summary>
/// <remarks>
/// Expected format of file is an xml-based file with first four lines as plain string data.
/// Example/description of the layout is as follows:
/// <example>
/// "<![CDATA[<!--
///  DATA_TYPE=XML
///  DATA_VERSION = 1
/// -->]]>"
/// The entry of the xml-based data that follows should be the <![CDATA[<Show/>]]> element,
/// holding the data of the playlist in raw string format, the air dates per channel as xml-data and
/// the templates as raw string data per social network.
/// </example>
/// </remarks>
public sealed class XmlDatav1ShowParser : IShowParser
{
    private readonly IPlaylistParser _playlistParser;

    /// <summary>
    /// Creates a new <see cref="XmlDatav1ShowParser"/>.
    /// </summary>
    /// <param name="playlistParser"></param>
    public XmlDatav1ShowParser(IPlaylistParser playlistParser)
        => _playlistParser = Guard.Against.Null(playlistParser);

    /// <inheritdoc />
    public Show Parse(FileInfo showDataFile)
    {
        Guard.Against.Null(showDataFile);

        if (!File.Exists(showDataFile.FullName))
        {
            var fileNotFoundException = new FileNotFoundException($"File '{showDataFile.Name}' does not exist, or could not be opened.");
            throw new ArgumentException(fileNotFoundException.Message, nameof(showDataFile), fileNotFoundException);
        }

        if (FileIsNullEmptyOrWhiteSpace(showDataFile))
            return Show.Empty;

        var header = TryParseHeader(showDataFile);
        var showXmlData = TryGetXmlData(showDataFile);
        var show = Show.Create(header);

        if (showXmlData is null
            || showXmlData.InnerXml.Equals("<Show />")
            || showXmlData.InnerXml.Equals("<Show/>")
            || showXmlData.InnerXml.Equals("<Show></Show>")
        )
            return show;

        ParseShow(showXmlData, show);
        ParseShowPlaylist(showXmlData, show);

        return show;
    }

    private void ParseShow(XmlDocument showXmlDocument, Show show)
    {
        var showXmlData = showXmlDocument.DocumentElement;

        var name = showXmlData.Attributes["Name"];
        if (name is null)
            throw new ArgumentException("Could not find 'Name' argument in <Show />");

        show.ChangeName(name.Value);

        var number = showXmlData.Attributes["EpisodeNumber"];
        if (number is not null)
            show.ChangeNumber(Int32.Parse(number.Value));
    }

    private void ParseShowPlaylist(XmlDocument showXmlData, Show show)
    {
        var playlistXmlData = showXmlData.DocumentElement!.SelectSingleNode("//Show/Playlist");
        var playlistStringData = playlistXmlData?.FirstChild?.Value ?? String.Empty;
        var playlist = _playlistParser.Parse(playlistStringData);
        show.ReplacePlaylist(playlist);
    }

    private bool FileIsNullEmptyOrWhiteSpace(FileInfo file)
        => GetCleanedLines(file)
            .Count() == 0;

    private ShowHeaderInfo TryParseHeader(FileInfo file)
    {
        var headerLines = Guard.Against.InvalidInput(
            GetCleanedLines(file).Take(4).ToList(),
            nameof(file), hls => hls.Count() == 4,
            "Header is not correctly formed."
        ).ToArray();

        if (headerLines[0] != "<!--")
            throw new ArgumentException(
                $"Header is not correctly formed at line 1, expected opening statement to be '<!--' but was '{headerLines[0]}'.",
                nameof(file)
            );
        if (!headerLines[1].Contains("DATA_TYPE="))
            throw new ArgumentException(
                $"Header is not correctly formed at line 2, expected statement to be 'DATA_TYPE' but was '{headerLines[1]}'.",
                nameof(file)
            );
        if (!headerLines[2].Contains("DATA_VERSION="))
            throw new ArgumentException(
                $"Header is not correctly formed at line 3, expected statement to be 'DATA_VERSION' but was '{headerLines[2]}'.",
                nameof(file)
            );
        if (headerLines[3] != "-->")
            throw new ArgumentException(
                $"Header is not correctly formed at line 4, expected closing statement to be '-->' but was '{headerLines[3]}'.",
                nameof(file)
            );

        Guard.Against.InvalidInput(
            headerLines[1],
            nameof(file), l => l == "DATA_TYPE=XML",
            "Header is not correctly formed, expected 'DATA_TYPE={type}' where 'type' expected to be 'XML'."
        );

        Guard.Against.InvalidInput(
            headerLines[2],
            nameof(file), l => l.Contains("DATA_VERSION="),
            "Header is not correctly formed, expected 'DATA_VERSION={versionNumber}' where 'versionNumber' expected be '1'."
        );

        var version = Int32.Parse(headerLines[2].Replace("DATA_VERSION=", ""));
        if (version != 1)
            throw new ArgumentException(
                $"Header is not correctly formed at line 3, expected version to be '1' but was '{version}'.",
                nameof(file)
            );

        return ShowHeaderInfo.Create("XML", version, file);
    }

    private XmlDocument TryGetXmlData(FileInfo file)
    {
        var xmlString = String.Join(
                Environment.NewLine,
                GetCleanedLines(file).Skip(4)
            );

        if (String.IsNullOrWhiteSpace(xmlString))
            xmlString = $"<Show />";

        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlString);

        return xmlDocument;
    }

    private static List<string> GetCleanedLines(FileInfo file)
        => File.ReadAllLines(file.FullName)
            .Where(line => !String.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim())
            .ToList();
}
