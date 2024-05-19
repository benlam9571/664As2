namespace LibraryManagement
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int Duration { get; set; }
        public int BorrowCount { get; set; }
        public int NumberOfCopies { get; set; }

        public Movie(string title, string genre, string classification, int duration, int numberOfCopies)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            NumberOfCopies = numberOfCopies;
        }
    }
}
