using System;

namespace LibraryManagement
{
    public class MovieCollection
    {
        private CustomList<Movie> movies = new CustomList<Movie>();

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }

        public bool RemoveMovie(string title)
        {
            var movie = movies.Find(m => m.Title == title);
            if (movie != null)
            {
                movies.Remove(movie);
                return true;
            }
            return false;
        }

        public Movie FindMovie(string title)
        {
            return movies.Find(m => m.Title == title);
        }

        public void DisplayAllMovies()
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"Title: {movie.Title}, Genre: {movie.Genre}, Copies: {movie.NumberOfCopies}");
            }
        }
    }
}
