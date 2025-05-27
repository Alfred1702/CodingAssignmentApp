using CodingAssignmentLib.Abstractions;
using System;

namespace CodingAssignmentLib;

public class CsvContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        return content.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).Select(line =>
        {
            var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return new Data(items[0], items[1]);
        });
    }
}