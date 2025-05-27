using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class JsonContentParserTests
{
    private JsonContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new JsonContentParser();
    }

    [Test]
    public void Parse_ValidJson_ReturnsCorrectData()
    {
        // Arrange
        var content = @"[
            {""Key"":""a"",""Value"":""b""},
            {""Key"":""c"",""Value"":""d""}
        ]";

        // Act
        var dataList = _sut.Parse(content).ToList();

        // Assert
        Assert.That(dataList.Count, Is.EqualTo(2));
        Assert.That(dataList[0].Key, Is.EqualTo("a"));
        Assert.That(dataList[0].Value, Is.EqualTo("b"));
        Assert.That(dataList[1].Key, Is.EqualTo("c"));
        Assert.That(dataList[1].Value, Is.EqualTo("d"));
    }
}
