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
        => throw new NotImplementedException();
}
