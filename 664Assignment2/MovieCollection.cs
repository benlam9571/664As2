public class MovieCollection
{
    private const int MaxSize = 1000;
    private Node[] table;

    public MovieCollection()
    {
        table = new Node[MaxSize];
    }

    private int GetHash(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash = (hash * 31 + c) % MaxSize;
        }
        return hash;
    }

    public void AddMovie(string title, Movie movie)
    {
        int index = GetHash(title);
        Node newNode = new Node(title, movie);

        if (table[index] == null)
        {
            table[index] = newNode;
        }
        else
        {
            Node current = table[index];
            while (current.Next != null)
            {
                if (current.Key == title)
                {
                    current.Value = movie;
                    return;
                }
                current = current.Next;
            }
            if (current.Key == title)
            {
                current.Value = movie;
            }
            else
            {
                current.Next = newNode;
            }
        }
    }

    public bool RemoveMovie(string title)
    {
        int index = GetHash(title);
        Node current = table[index];
        Node previous = null;

        while (current != null)
        {
            if (current.Key == title)
            {
                if (previous == null)
                {
                    table[index] = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public Movie GetMovie(string title)
    {
        int index = GetHash(title);
        Node current = table[index];

        while (current != null)
        {
            if (current.Key == title)
            {
                return current.Value;
            }
            current = current.Next;
        }
        return null;
    }

    public bool ContainsMovie(string title)
    {
        return GetMovie(title) != null;
    }

    public void DisplayAllMovies()
    {
        Node[] allMovies = GetAllNodes();
        Array.Sort(allMovies, (x, y) => string.Compare(x.Key, y.Key));

        foreach (var node in allMovies)
        {
            Console.WriteLine(node.Value);
        }
    }

    public Node[] GetAllNodes()
    {
        Node[] nodes = new Node[MaxSize];
        int count = 0;

        for (int i = 0; i < MaxSize; i++)
        {
            Node current = table[i];
            while (current != null)
            {
                nodes[count++] = current;
                current = current.Next;
            }
        }

        Node[] result = new Node[count];
        Array.Copy(nodes, result, count);
        return result;
    }

    public void IncrementBorrowCount(string title)
    {
        Movie movie = GetMovie(title);
        if (movie != null)
        {
            movie.BorrowCount++;
        }
    }
}
