namespace CodingAssignmentLib.Abstractions;

public interface ISearch
{
    IEnumerable<(Data data, string filePath)> Search(string key, IEnumerable<string> filePaths);
}