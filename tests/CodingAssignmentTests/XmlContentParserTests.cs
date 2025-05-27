using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class XmlContentParserTests
{
    private XmlContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new XmlContentParser();
    }

    [Test]
    public void Parse_ValidXml_ReturnsCorrectData()
    {
        // Arrange
        var content = @"
<Datas>
  <Data>
    <Key>a</Key>
    <Value>b</Value>
  </Data>
  <Data>
    <Key>c</Key>
    <Value>d</Value>
  </Data>
</Datas>";

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
