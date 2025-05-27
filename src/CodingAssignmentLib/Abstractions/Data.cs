namespace CodingAssignmentLib.Abstractions;

public struct Data
{
    public Data(string key, string value)
    {
        Key = key;
        Value = value;
    }
    public string Key { get; set; }
    public string Value { get; set; }

    public Data() { } // Required for XML serialization


}