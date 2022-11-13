﻿namespace Kf.Eclectricast.PlaylistManager.Infrastructure.Persistence.Xml.Tests.Shows;

public sealed class XmlDatav1ShowParserTests
{
    [Fact]
    public void File_parameter_that_is_null_throws_ArgumentException()
    {
        var exception = Should.Throw<ArgumentException>(
            () => new XmlDatav1ShowParser().Parse((FileInfo)null!)
        );

        exception.ParamName.ShouldBe("showDataFile");
    }

    [Fact]
    public void File_that_does_not_exist_throws_ArgumentException()
    {
        var exception = Should.Throw<ArgumentException>(
            () => new XmlDatav1ShowParser().Parse(GetDataSheetXmlFile(""))
        );

        exception.ParamName.ShouldBe("showDataFile");
    }

    [Theory]
    [InlineData("Show-InvalidHeader-NoComments", "at line 1, expected opening statement to be '<!--'")]
    [InlineData("Show-InvalidHeader-NoCaps", "at line 2, expected statement to be 'DATA_TYPE'")]
    [InlineData("Show-InvalidHeader-WrongOrder", "at line 2, expected statement to be 'DATA_TYPE'")]
    [InlineData("Show-InvalidHeader-WrongVersion", "expected 'DATA_VERSION={versionNumber}' where 'versionNumber' expected be '1'")]
    [InlineData("Show-InvalidHeader-OneLiner", "at line 1, expected opening statement to be '<!--'")]
    public void File_with_invalid_header_throws_ArgumentException(
        string name, string error
    )
    {
        var exception = Should.Throw<ArgumentException>(
            () => new XmlDatav1ShowParser().Parse(GetDataSheetXmlFile(name))
        );

        exception.ParamName.ShouldBe("file");
        exception.Message.ShouldContain("Header is not correctly formed");
        exception.Message.ShouldContain(error);
    }

    [Theory]
    [InlineData("Show-Empty")]
    [InlineData("Show-EmptyLines")]
    public void Empty_file_returns_empty_show(string name)
        => new XmlDatav1ShowParser()
            .Parse(GetDataSheetXmlFile(name))
            .ShouldBe(Show.Empty);


    public static FileInfo GetDataSheetXmlFile(string name)
        => new FileInfo(
                Path.Combine(
                    Environment.CurrentDirectory,
                    "Shows",
                    "ShowDataSheets",
                    $"{name}.DataSheet.xml"
                )
            );
}