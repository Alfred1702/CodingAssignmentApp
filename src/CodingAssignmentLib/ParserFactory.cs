using CodingAssignmentLib.Abstractions;


namespace CodingAssignmentLib;


public class ParserFactory : IParserFactory
{
    public IContentParser? GetParser(string extension)
    {
        return extension.ToLower() switch
        {
            ".csv" => new CsvContentParser(),
            ".json" => new JsonContentParser(),
            ".xml" => new XmlContentParser(),
            _ => null
        };
    }
}