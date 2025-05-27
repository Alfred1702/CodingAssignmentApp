using CodingAssignmentLib.Abstractions;

public interface IParserFactory
{
    IContentParser? GetParser(string extension);
}