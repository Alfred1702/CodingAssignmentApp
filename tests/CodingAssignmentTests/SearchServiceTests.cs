using CodingAssignmentLib;

public class SearchServiceTests
{

    private string _testDirectory = null!;
    private StringWriter _outputWriter = null!;
    private SearchService _sut = null!;

    [SetUp]
    public void Setup()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_testDirectory);

        // Create test JSON file
        File.WriteAllText(Path.Combine(_testDirectory, "test.json"),
            "[{\"Key\":\"foo\",\"Value\":\"bar\"}]");

        // Create test CSV file
        File.WriteAllText(Path.Combine(_testDirectory, "test.csv"),
            "foo,bar");

        // Create test XML file
        File.WriteAllText(Path.Combine(_testDirectory, "test.xml"),
            @"<Datas>
                  <Data>
                    <Key>foo</Key>
                    <Value>bar</Value>
                  </Data>
                  <Data>
                    <Key>baz</Key>
                    <Value>qux</Value>
                  </Data>
                </Datas>");

        _outputWriter = new StringWriter();
        Console.SetOut(_outputWriter);

        _sut = new SearchService(new ParserFactory());
    }

    [TearDown]
    public void Cleanup()
    {
        if (Directory.Exists(_testDirectory))
            Directory.Delete(_testDirectory, true);

        _outputWriter.Dispose();
    }

    [TestCase(".json")]
    [TestCase(".csv")]
    [TestCase(".xml")]
    public void SSearch_PrintsMatchingRecord_ForVariousFormats(string extension)
    {
        // Act
        _sut.Search("foo", _testDirectory);

        // Assert
        var output = _outputWriter.ToString();
        Assert.That(output, Does.Contain("Key:foo Value:bar"));
        Assert.That(output, Does.Contain($"test{extension}"));
    }

    [Test]
    public void Search_IgnoresNonMatchingRecords()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        _sut.Search("nonexistentKey", _testDirectory);

        // Assert
        var output = sw.ToString();

        Assert.That(output, Does.Not.Contain("Key:"));
    }
}