using CodingAssignmentLib.Abstractions;
using System.Xml.Serialization;

namespace CodingAssignmentLib;


[XmlRoot("Datas")]
public class DataList
{
    [XmlElement("Data")]
    public List<Data> Data { get; set; }
}

public class XmlContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        var serializer = new XmlSerializer(typeof(DataList));
        using var reader = new StringReader(content);
        var result = serializer.Deserialize(reader) as DataList;
        return result?.Data ?? Enumerable.Empty<Data>();
    }
}