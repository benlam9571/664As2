using System;

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

        if (username == "staff" && password == "today123")
        {
            StaffMenu();
        }
        else
        {
            Console.WriteLine("Invalid staff credentials.");
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
                    break;
            }
        }
    }

    private static void MemberLogin()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null && member.VerifyPassword(password))
        {
            MemberMenu(member);
        }
        else
        {
            Console.WriteLine("Invalid member credentials.");
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

            switch (option)
            {
                case "1":
                    DisplayAllMovies();
                    break;
                case "2":
                    DisplayMovieInfo();
                    break;
                case "3":
                    BorrowMovie(member);
                    break;
                case "4":
                    ReturnMovie(member);
                    break;
                case "5":
                    ListBorrowedMovies(member);
                    break;
                case "6":
                    DisplayTopBorrowedMovies();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Staff menu methods

    private static void AddMovie()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();
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
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                for (int i = 0; i < copies; i++)
                {
                    movieCollection.AddMovie(title, movie);
                }
            }
            else
            {
                movie = new Movie(title, genre, classification, duration);
                for (int i = 0; i < copies; i++)
                {
                    movieCollection.AddMovie(title, movie);
                }
            }

            Console.WriteLine("Movie(s) added successfully.");
        }
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

        if (title != null)
        {
            for (int i = 0; i < copies; i++)
            {
                if (!movieCollection.RemoveMovie(title))
                {
                    Console.WriteLine("Not enough copies to remove or movie does not exist.");
                    return;
                }
            }

            Console.WriteLine("Movie(s) removed successfully.");
        }
    }

    private static void RegisterMember()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter contact phone number: ");
        string phoneNumber = Console.ReadLine();
        Console.Write("Enter 4-digit password: ");
        string password = Console.ReadLine();

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
    }

    private static void RemoveMember()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null)
        {
            if (member.BorrowedMovies.Length == 0)
            {
                memberCollection.RemoveMember(firstName, lastName);
                Console.WriteLine("Member removed successfully.");
            }
            else
            {
                Console.WriteLine("Member has borrowed movies. Cannot remove.");
            }
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
    }

    private static void FindMemberContact()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Member? member = memberCollection.FindMember(firstName, lastName);
        if (member != null)
        {
            Console.WriteLine($"Contact phone number: {member.ContactPhoneNumber}");
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
    }

    private static void FindMembersRentingMovie()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();

        if (title != null)
        {
            Console.WriteLine($"Members currently renting '{title}':");
            foreach (Member member in memberCollection.GetAllMembers())
            {
                foreach (Movie? movie in member.BorrowedMovies)
                {
                    if (movie != null && movie.Title == title)
                    {
                        Console.WriteLine($"{member.FirstName} {member.LastName}");
                        break;
                    }
                }
            }
        }
    }

    // Member menu methods

    private static void DisplayAllMovies()
    {
        Console.Clear();
        movieCollection.DisplayAllMovies();
    }

    private static void DisplayMovieInfo()
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();

        if (title != null)
        {
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                Console.WriteLine(movie);
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }
    }

    private static void BorrowMovie(Member member)
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();

        if (title != null)
        {
            Movie? movie = movieCollection.GetMovie(title);
            if (movie != null)
            {
                if (member.BorrowMovie(movie))
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
    }

    private static void ReturnMovie(Member member)
    {
        Console.Write("Enter movie title: ");
        string? title = Console.ReadLine();

        if (title != null && member.ReturnMovie(title))
        {
            Console.WriteLine("Movie returned successfully.");
        }
        else
        {
            Console.WriteLine("Movie not found in your borrowed list.");
        }
    }

    private static void ListBorrowedMovies(Member member)
    {
        member.ListBorrowedMovies();
    }

    private static void DisplayTopBorrowedMovies()
    {
        Node[] allMovies = movieCollection.GetAllNodes();
        Array.Sort(allMovies, (x, y) => y.Value.BorrowCount.CompareTo(x.Value.BorrowCount));

        Console.WriteLine("Top 3 Most Borrowed Movies:");
        for (int i = 0; i < Math.Min(3, allMovies.Length); i++)
        {
            Console.WriteLine($"{allMovies[i].Value.Title}: {allMovies[i].Value.BorrowCount} times");
        }
    }
}
