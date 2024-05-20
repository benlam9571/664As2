public class Node
{
    public string Key { get; set; }
    public Movie Value { get; set; }
    public Node Next { get; set; }

    public Node(string key, Movie value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
}
