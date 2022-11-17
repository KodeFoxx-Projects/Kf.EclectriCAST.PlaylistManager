namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Tests.Shows;

public sealed class XmlDatav1ShowParserTests
{
    [Theory]
    [InlineData("Show")]
    [InlineData("Show-OnlyHeader")]
    public void Header_for_valid_file_gets_parsed_in_show(string name)
    {
        var sut = GetXmlDataShowParser().Parse(GetDataSheetXmlFile(name));

        sut.Header.ShouldNotBeNull();
        sut.Header.Format.ShouldBe("XML");
        sut.Header.Version.ShouldBe(1);
        sut.Header.File.Name.ShouldBe($"{name}.DataSheet.xml");
    }

    [Fact]
    public void File_parameter_that_is_null_throws_ArgumentException()
    {
        var exception = Should.Throw<ArgumentException>(
            () => GetXmlDataShowParser().Parse((FileInfo)null!)
        );

        exception.ParamName.ShouldBe("showDataFile");
    }

    [Fact]
    public void Valid_file_playlist_should_be_filled()
    {
        var sut = GetXmlDataShowParser().Parse(GetDataSheetXmlFile("Show"));

        sut.Playlist.ShouldNotBeNull();
        sut.Playlist.HasSegments.ShouldBeTrue();
        sut.Playlist.Segments.Count().ShouldBe(20);
    }

    [Fact]
    public void Valid_episodic_file_should_contain_number()
    {
        var sut = GetXmlDataShowParser().Parse(GetDataSheetXmlFile("Show"));

        sut.Number.ShouldBe(99);
        sut.IsEpisode.ShouldBeTrue();
    }

    [Fact]
    public void Valid_file_should_contain_name()
    {
        var sut = GetXmlDataShowParser().Parse(GetDataSheetXmlFile("Show"));

        sut.Name.ShouldBe("The EclectriCAST");
    }

    [Fact]
    public void File_that_does_not_exist_throws_ArgumentException()
    {
        var exception = Should.Throw<ArgumentException>(
            () => GetXmlDataShowParser().Parse(GetDataSheetXmlFile(""))
        );

        exception.ParamName.ShouldBe("showDataFile");
    }

    [Theory]
    [InlineData("Show-InvalidHeader-NoComments", "at line 1, expected opening statement to be '<!--'")]
    [InlineData("Show-InvalidHeader-NoCaps", "at line 2, expected statement to be 'DATA_TYPE'")]
    [InlineData("Show-InvalidHeader-WrongOrder", "at line 2, expected statement to be 'DATA_TYPE'")]
    [InlineData("Show-InvalidHeader-WrongVersion", "at line 3, expected version to be '1'")]
    [InlineData("Show-InvalidHeader-OneLiner", "at line 1, expected opening statement to be '<!--'")]
    public void File_with_invalid_header_throws_ArgumentException(
        string name, string error
    )
    {
        var exception = Should.Throw<ArgumentException>(
            () => GetXmlDataShowParser().Parse(GetDataSheetXmlFile(name))
        );

        exception.ParamName.ShouldBe("file");
        exception.Message.ShouldContain("Header is not correctly formed");
        exception.Message.ShouldContain(error);
    }

    [Theory]
    [InlineData("Show-Empty")]
    [InlineData("Show-EmptyLines")]
    public void Empty_file_returns_empty_show(string name)
        => GetXmlDataShowParser()
            .Parse(GetDataSheetXmlFile(name))
            .ShouldBe(Show.Empty);


    public static FileInfo GetDataSheetXmlFile(string name)
        => new FileInfo(
                Path.Combine(
                    Environment.CurrentDirectory,
                    "TestData",
                    "Xmlv1",
                    "ShowDataSheets",
                    $"{name}.DataSheet.xml"
                )
            );

    private XmlDatav1ShowParser GetXmlDataShowParser()
        => new XmlDatav1ShowParser(
            new XmlDatav1PlaylistParser(
                new XmlDatav1SegmentParser()));
}