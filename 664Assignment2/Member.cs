namespace LibraryManagement
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public CustomList<Movie> BorrowedMovies { get; set; } = new CustomList<Movie>();

        public Member(string firstName, string lastName, string phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        
        public void BorrowMovie(Movie movie)
        {
            BorrowedMovies.Add(movie);
        }

        public void ReturnMovie(Movie movie)
        {
            BorrowedMovies.Remove(movie);
        }

    }
}
