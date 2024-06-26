﻿using System;

class Program
{
    private static MovieCollection movieCollection = new MovieCollection();
    private static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine("=================================================");
            Console.WriteLine("Main Menu");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Select from the following:");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. End the program");
            Console.Write("Enter your choice ==> ");
            string option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    StaffLogin();
                    break;
                case "2":
                    MemberLogin();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("** Press any key to continue **");
                    Console.Read();
                    break;
            }
        }
    }

    private static void StaffLogin()
    {
        Console.Write("Enter staff username: ");
        string username = Console.ReadLine();
        Console.Write("Enter staff password: ");
        string password = Console.ReadLine();
        Console.Clear();

        if (username == "staff" && password == "today123")
        {
            StaffMenu();
        }
        else
        {
            Console.WriteLine("Invalid staff username/password.");
            Console.WriteLine("** Press any key to continue **");
            Console.Read();
            Console.Clear();
        }
    }

    private static void StaffMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Staff Menu");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1. Add DVDs to system");
            Console.WriteLine("2. Remove DVDs from system");
            Console.WriteLine("3. Register a new member to system");
            Console.WriteLine("4. Remove a registered member from system");
            Console.WriteLine("5. Find a member contact phone number, given the member's name");
            Console.WriteLine("6. Find members who are currently renting a particular movie");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");
            string option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    AddMovie();
                    break;
                case "2":
                    RemoveMovie();
                    break;
                case "3":
                    RegisterMember();
                    break;
                case "4":
                    RemoveMember();
                    break;
                case "5":
                    FindMemberContact();
                    break;
                case "6":
                    FindMembersRentingMovie();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("** Press any key to continue **");
                    Console.Read();
                    break;
            }
        }
    }

    private static void MemberLogin()
    {
        Console.Write("Enter First name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();
        Console.Clear();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null && member.VerifyPassword(password))
        {
            MemberMenu(member);
        }
        else
        {
            Console.WriteLine("Invalid member name / password.");
            Console.WriteLine("** Press any key to continue **");
            Console.Read();
            Console.Clear();
        }
    }

    private static void MemberMenu(Member member)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Member Menu");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");
            string option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    DisplayAllMovies();
                    break;
                case "2":
                    DisplayMovieInfo();
                    break;
                case "3":
                    BorrowDVD(member);
                    break;
                case "4":
                    ReturnMovie(member);
                    break;
                case "5":
                    ListBorrowedMovies(member);
                    break;
                case "6":
                    DisplayTop3BorrowedMovies();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("** Press any key to continue **");
                    Console.Read();
                    break;
            }
        }
    }

    private static void AddMovie()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("No movie title entered. Please try again.");
            Console.WriteLine("** Press any key to continue **");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        Console.Write("Enter movie genre (Drama, Adventure, Family, Action, SciFi, Comedy, Animated, Thriller, Other): ");
        Genre genre;
        while (!Enum.TryParse(Console.ReadLine(), out genre))
        {
            Console.Write("Invalid genre. Enter again: ");
        }
        Console.Write("Enter movie classification (G, PG, M15, MA15): ");
        Classification classification;
        while (!Enum.TryParse(Console.ReadLine(), out classification))
        {
            Console.Write("Invalid classification. Enter again: ");
        }
        Console.Write("Enter movie duration (minutes): ");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration))
        {
            Console.Write("Invalid duration. Enter again: ");
        }
        Console.Write("Enter number of copies: ");
        int copies;
        while (!int.TryParse(Console.ReadLine(), out copies))
        {
            Console.Write("Invalid number of copies. Enter again: ");
        }

        if (title != null)
        {
            Movie? existingMovie = movieCollection.GetMovie(title);
            if (existingMovie != null)
            {
                existingMovie.NumberOfCopies += copies;
                Console.WriteLine($"Movie '{title}' already exists. Updated number of copies to {existingMovie.NumberOfCopies}.");
            }
            else
            {
                Movie movie = new Movie(title, genre, classification, duration, copies);
                movieCollection.AddMovie(title, movie);
                Console.WriteLine("Movie(s) added successfully.");
            }
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }


    private static void RemoveMovie()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        Console.Write("Enter number of copies to remove: ");
        int copies;
        while (!int.TryParse(Console.ReadLine(), out copies))
        {
            Console.Write("Invalid number of copies. Enter again: ");
        }
        Console.Clear();

        if (title != null)
        {
            for (int i = 0; i < copies; i++)
            {
                if (!movieCollection.RemoveMovie(title))
                {
                    Console.WriteLine("Not enough copies to remove or movie does not exist.");
                    Console.WriteLine("** Press any key to continue **");
                    Console.Read();
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("Movie(s) removed successfully.");
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void RegisterMember()
    {
        Console.Write("Enter First name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter contact phone number: ");
        string phoneNumber = Console.ReadLine();

        string password;
        while (true)
        {
            Console.Write("Enter 4-digit password: ");
            password = Console.ReadLine();
            if (password.Length == 4 && int.TryParse(password, out _))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid password. The password must be a 4-digit number.");
            }
        }
        Console.Clear();

        if (memberCollection.FindMember(firstName, lastName) == null)
        {
            Member member = new Member(firstName, lastName, phoneNumber, password);
            memberCollection.AddMember(member);
            Console.WriteLine("Member registered successfully.");
        }
        else
        {
            Console.WriteLine("Member already exists.");
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void RemoveMember()
    {
        Console.Write("Enter First name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last name: ");
        string lastName = Console.ReadLine();
        Console.Clear();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null)
        {
            if (member.BorrowedCount > 0)
            {
                Console.WriteLine("Member cannot be removed. They must return all borrowed DVDs first.");
            }
            else
            {
                memberCollection.RemoveMember(firstName, lastName);
                Console.WriteLine("Member removed successfully.");
            }
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void FindMemberContact()
    {
        Console.Write("Enter First name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last name: ");
        string lastName = Console.ReadLine();
        Console.Clear();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null)
        {
            Console.WriteLine($"Contact Phone Number: {member.ContactPhoneNumber}");
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void FindMembersRentingMovie()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        Console.Clear();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("No movie title entered. Please try again.");
        }
        else
        {
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                Console.WriteLine($"Members currently renting '{title}':");
                bool found = false;
                foreach (Member member in memberCollection.GetAllMembers())
                {
                    foreach (Movie? borrowedMovie in member.BorrowedMovies)
                    {
                        if (borrowedMovie != null && borrowedMovie.Title == title)
                        {
                            Console.WriteLine($"{member.FirstName} {member.LastName}");
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    Console.WriteLine($"No members are currently renting '{title}'.");
                }
            }
            else
            {
                Console.WriteLine($"Movie '{title}' not available in the library.");
            }
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }


    private static void DisplayAllMovies()
    {
        if (movieCollection.Count == 0)
        {
            Console.WriteLine("No movies available in the library.");
        }
        else
        {
            movieCollection.DisplayAllMovies();
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }


    private static void DisplayMovieInfo()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        Console.WriteLine(); // Add a blank line before the output

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("No movie title entered. Please try again.");
        }
        else
        {
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                Console.WriteLine(movie.ToDetailedString());
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }
        Console.WriteLine(); // Add a blank line after the output
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }


    private static void BorrowDVD(Member member)
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        Console.Clear();

        if (title != null)
        {
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                if (member.BorrowDVD(movie))
                {
                    movieCollection.IncrementBorrowCount(title);
                    Console.WriteLine("Movie borrowed successfully.");
                }
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void ReturnMovie(Member member)
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
        Console.Clear();

        if (title != null && member.ReturnMovie(title))
        {
            Console.WriteLine("Movie returned successfully.");
        }
        else
        {
            Console.WriteLine("Movie not found in your borrowed list.");
        }
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    private static void ListBorrowedMovies(Member member)
    {
        member.ListBorrowedMovies();
        Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }
    private static void DisplayTop3BorrowedMovies()
    {
        Movie[] movies = movieCollection.GetAllMovies();
        if (movies.Length == 0)
        {
            Console.WriteLine("No movies in the collection.");
            return;
        }

        MergeSort(movies, 0, movies.Length - 1);

        Console.WriteLine("Top 3 Most Borrowed Movies:");
        for (int i = 0; i < Math.Min(3, movies.Length); i++)
        {
            Console.WriteLine($"{movies[i].Title}: {movies[i].BorrowCount} times");
        }
         Console.WriteLine("** Press any key to continue **");
        Console.Read();
        Console.Clear();
    }

    static void MergeSort(Movie[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    static void Merge(Movie[] array, int left, int middle, int right)
    {
        int leftSize = middle - left + 1;
        int rightSize = right - middle;

        Movie[] leftArray = new Movie[leftSize];
        Movie[] rightArray = new Movie[rightSize];

        for (int i = 0; i < leftSize; i++)
            leftArray[i] = array[left + i];
        for (int i = 0; i < rightSize; i++)
            rightArray[i] = array[middle + 1 + i];

        int iLeft = 0, iRight = 0;
        int k = left;
        while (iLeft < leftSize && iRight < rightSize)
        {
            if (leftArray[iLeft].BorrowCount >= rightArray[iRight].BorrowCount)
            {
                array[k] = leftArray[iLeft];
                iLeft++;
            }
            else
            {
                array[k] = rightArray[iRight];
                iRight++;
            }
            k++;
        }

        while (iLeft < leftSize)
        {
            array[k] = leftArray[iLeft];
            iLeft++;
            k++;
        }

        while (iRight < rightSize)
        {
            array[k] = rightArray[iRight];
            iRight++;
            k++;
        }
    }
}