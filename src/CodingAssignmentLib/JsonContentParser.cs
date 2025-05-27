using CodingAssignmentLib.Abstractions;
using System.Text.Json;

namespace CodingAssignmentLib;

public class JsonContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        try
        {
            return JsonSerializer.Deserialize<List<Data>>(content);
        }
        catch (JsonException e)
        {
            return Enumerable.Empty<Data>();
        }
    }
}