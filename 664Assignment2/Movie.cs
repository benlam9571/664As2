public enum Genre
{
    Drama,
    Adventure,
    Family,
    Action,
    SciFi,
    Comedy,
    Animated,
    Thriller,
    Other
}

public enum Classification
{
    G,      // General
    PG,     // Parental Guidance
    M15,    // Mature (M15+)
    MA15    // Mature Accompanied (MA15+)
}

public class Movie
{
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public Classification Classification { get; set; }
    public int Duration { get; set; }
    public int BorrowCount { get; set; }
    public int NumberOfCopies { get; set; }

    public Movie(string title, Genre genre, Classification classification, int duration, int numberOfCopies)
    {
        Title = title;
        Genre = genre;
        Classification = classification;
        Duration = duration;
        BorrowCount = 0;
        NumberOfCopies = numberOfCopies;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration} minutes, Borrowed: {BorrowCount} times, Copies: {NumberOfCopies}";
    }
}