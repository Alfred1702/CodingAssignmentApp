// See https://aka.ms/new-console-template for more information

using System.Configuration;
using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

Console.WriteLine("Coding Assignment!");

do
{
    Console.WriteLine("\n---------------------------------------\n");
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\t1 - Display");
    Console.WriteLine("\t2 - Search");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            Display();
            break;
        case "2":
            Search();
            break;
        case "3":
            return;
        default:
            return;
    }
} while (true);


void Display()
{
    Console.WriteLine("Enter the name of the file to display its content:");

    var fileName = Console.ReadLine()!;
    var fileUtility = new FileUtility(new FileSystem());
    var dataList = Enumerable.Empty<Data>();

    if (fileUtility.GetExtension(fileName) == ".csv")
    {
        dataList = new CsvContentParser().Parse(fileUtility.GetContent(fileName));
    }
    else if(fileUtility.GetExtension(fileName) == ".json")
    {
        dataList = new JsonContentParser().Parse(fileUtility.GetContent(fileName));
    }
    else if (fileUtility.GetExtension(fileName) == ".xml")
    {
        dataList = new XmlContentParser().Parse(fileUtility.GetContent(fileName));
    }
    else
    {
        Console.WriteLine("Please input the correct file format.");
    }

    Console.WriteLine("Data:");

    foreach (var data in dataList)
    {
        Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
    }
}

void Search()
{
    Console.WriteLine("Enter the key to search:");
    var fileKey = Console.ReadLine();

    string filePath = ConfigurationManager.AppSettings["DataFilePath"];

    if (string.IsNullOrWhiteSpace(filePath))
    {
        Console.WriteLine("File path not found in config.");
        return;
    }

    var files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
                        .Where(f => f.EndsWith(".csv") || f.EndsWith(".json") || f.EndsWith(".xml"));

    IParserFactory parserFactory = new ParserFactory();
    var searchService = new SearchService(parserFactory);

    searchService.Search(fileKey, filePath);
}