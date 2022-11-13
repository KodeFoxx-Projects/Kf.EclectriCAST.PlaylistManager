using Ardalis.GuardClauses;

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
    /// <inheritdoc />    
    public Show Parse(string show)
        => throw new NotImplementedException();

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

        var headerInfo = TryParseHeader(showDataFile);

        throw new NotImplementedException();
    }

    private bool FileIsNullEmptyOrWhiteSpace(FileInfo file)
        => GetCleanedLines(file)
            .Count() == 0;

    private (string DataType, int Version) TryParseHeader(FileInfo file)
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
            headerLines[3],
            nameof(file), l => l.Contains("DATA_VERSION="),
            "Header is not correctly formed, expected 'DATA_VERSION={versionNumber}' where 'versionNumber' expected be '1'."
        );

        var version = Int32.Parse(headerLines[3].Replace("DATA_VERSION=", ""));

        return (
            DataType: "XML",
            Version: version
        );
    }

    private static List<string> GetCleanedLines(FileInfo file)
        => File.ReadAllLines(file.FullName)
            .Where(line => !String.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim())
            .ToList();
}
