using CodingAssignmentLib.Abstractions;

public class SearchService
{
    private readonly IParserFactory _parserFactory;

    public SearchService(IParserFactory parserFactory)
    {
        _parserFactory = parserFactory;
    }

    public void Search(string fileKey, string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path not found.");
            return;
        }

        var files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
            .Where(f => f.EndsWith(".csv") || f.EndsWith(".json") || f.EndsWith(".xml"));

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file);
            var parser = _parserFactory.GetParser(extension);
            if (parser == null) continue;

            string content = File.ReadAllText(file);
            IEnumerable<Data> records;
            try
            {
                records = parser.Parse(content);
            }
            catch
            {
                Console.WriteLine($"Failed to parse file: {file}");
                continue;
            }

            foreach (var record in records)
            {
                if (record.Key.Equals(fileKey, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Key:{record.Key} Value:{record.Value} FileName:{file}");
                }
            }
        }
    }
}