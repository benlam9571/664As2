public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactPhoneNumber { get; set; }
    private string Password { get; set; }
    public Movie[] BorrowedMovies { get; set; }
    private const int MaxBorrowedMovies = 5;
    public int BorrowedCount { get; private set; }

    public Member(string firstName, string lastName, string contactPhoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        ContactPhoneNumber = contactPhoneNumber;
        Password = password;
        BorrowedMovies = new Movie[MaxBorrowedMovies];
        BorrowedCount = 0;
    }

    public bool VerifyPassword(string password)
    {
        return Password == password;
    }

    public bool BorrowDVD(Movie movie)
{
    if (BorrowedCount >= MaxBorrowedMovies)
    {
        Console.WriteLine("Cannot borrow more than 5 movies at a time.");
        return false;
    }

    for (int i = 0; i < BorrowedCount; i++)
    {
        if (BorrowedMovies[i].Title == movie.Title)
        {
            Console.WriteLine("Cannot borrow more than one copy of the same movie.");
            return false;
        }
    }

    if (movie.NumberOfCopies > 0)
    {
        BorrowedMovies[BorrowedCount] = movie;
        BorrowedCount++;
        movie.NumberOfCopies--;
        return true;
    }
    else
    {
        Console.WriteLine("No copies available to borrow.");
        return false;
    }
}

    public bool ReturnMovie(string title)
{
    for (int i = 0; i < BorrowedCount; i++)
    {
        if (BorrowedMovies[i] != null && BorrowedMovies[i].Title == title)
        {
            BorrowedMovies[i].NumberOfCopies++; // Increment the number of copies
            BorrowedMovies[i] = BorrowedMovies[BorrowedCount - 1];
            BorrowedMovies[BorrowedCount - 1] = null;
            BorrowedCount--;
            return true;
        }
    }
    Console.WriteLine("Movie not found in the borrowed list.");
    return false;
}


    public void ListBorrowedMovies()
    {
        Console.WriteLine($"Borrowed movies by {FirstName} {LastName}:");
        for (int i = 0; i < BorrowedCount; i++)
        {
            Console.WriteLine(BorrowedMovies[i]);
        }
    }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}, Contact: {ContactPhoneNumber}, Borrowed Movies: {BorrowedCount}";
    }
}
