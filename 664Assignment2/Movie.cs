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

    public Movie(string title, Genre genre, Classification classification, int duration)
    {
        Title = title;
        Genre = genre;
        Classification = classification;
        Duration = duration;
        BorrowCount = 0;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration} minutes, Borrowed: {BorrowCount} times";
    }
}
