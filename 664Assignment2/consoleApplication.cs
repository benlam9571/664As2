using System;

namespace LibraryManagement
{
    public class DVDsManagementApp
    {
        private MovieCollection movies = new MovieCollection();
        private MemberCollection members = new MemberCollection();

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("=============================================================");
                Console.WriteLine("\nCOMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
                Console.WriteLine("=============================================================");
                Console.WriteLine("Main Menu");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Select from the following options:");
                
                Console.WriteLine("1. Staff");
                Console.WriteLine("2. Member");
                Console.WriteLine("0. End the program");
                Console.Write("Enter your choice ==> ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StaffLogin();
                        break;
                    case "2":
                        MemberLogin();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private void StaffLogin()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (username == "staff" && password == "today123")
            {
                StaffMenu();
            }
            else
            {
                Console.WriteLine("Incorrect credentials, returning to main menu.");
            }
        }

        private void MemberLogin()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var member = members.FindMember(firstName, lastName, password);
            if (member != null)
            {
                MemberMenu(member);
            }
            else
            {
                Console.WriteLine("Member not found or incorrect password.");
            }
        }

        private void StaffMenu()
        {
            bool stay = true;
            while (stay)
            {
                Console.WriteLine("\nStaff Menu");
                Console.WriteLine("--------------------------------");
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
                        FindRentingMembers();
                        break;
                    case "0":
                        stay = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private void MemberMenu(Member member)
        {
            bool stay = true;
            while (stay)
            {
                Console.WriteLine("\nMember Menu");
                Console.WriteLine("--------------------------------");
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
                        movies.DisplayAllMovies();
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
                        DisplayBorrowedMovies(member);
                        break;
                    case "6":
                        DisplayTopRentedMovies();
                        break;
                    case "0":
                        stay = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private void AddMovie()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Genre: ");
            string genre = Console.ReadLine();
            Console.Write("Enter Classification: ");
            string classification = Console.ReadLine();
            Console.Write("Enter Duration (in minutes): ");
            int duration = int.Parse(Console.ReadLine());
            Console.Write("Enter Number of Copies: ");
            int copies = int.Parse(Console.ReadLine());

            movies.AddMovie(new Movie(title, genre, classification, duration, copies));
            Console.WriteLine("Movie added successfully.");
        }

        private void RemoveMovie()
        {
            Console.Write("Enter Title of the Movie to remove: ");
            string title = Console.ReadLine();
            if (movies.RemoveMovie(title))
            {
                Console.WriteLine("Movie removed successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        private void RegisterMember()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            members.AddMember(new Member(firstName, lastName, phoneNumber, password));
            Console.WriteLine("Member registered successfully.");
        }

        private void RemoveMember()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            if (members.RemoveMember(firstName, lastName))
            {
                Console.WriteLine("Member removed successfully.");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        private void FindMemberContact()
        {
            Console.Write("Enter Member's First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Member's Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Member's Password: ");
            string password = Console.ReadLine();
            Member member = members.FindMember(firstName, lastName, password);
            if (member != null)
            {
                Console.WriteLine($"Member's Contact Number: {member.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        private void FindRentingMembers()
        {
            // Console.Write("Enter the Movie Title: ");
            // string title = Console.ReadLine();
            // Console.WriteLine("Members currently renting the movie:");
            // foreach (var member in members.FindAll(m => m.BorrowedMovies.Any(b => b.Title == title)))
            // {
            //     Console.WriteLine($"{member.FirstName} {member.LastName}");
            // }
        }

        private void DisplayMovieInfo()
        {
            Console.Write("Enter the Movie Title: ");
            string title = Console.ReadLine();
            var movie = movies.FindMovie(title);
            if (movie != null)
            {
                Console.WriteLine($"Title: {movie.Title}, Genre: {movie.Genre}, Classification: {movie.Classification}, Duration: {movie.Duration} mins, Copies: {movie.NumberOfCopies}");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        private void BorrowMovie(Member member)
        {
            // Console.Write("Enter the Title of the Movie to borrow: ");
            // string title = Console.ReadLine();
            // var movie = movies.FindMovie(title);
            // if (movie != null && movie.NumberOfCopies > 0 && member.BorrowMovie(movie))
            // {
            //     movie.NumberOfCopies--;
            //     Console.WriteLine("Movie borrowed successfully.");
            // }
            // else
            // {
            //     Console.WriteLine("Could not borrow movie. It may not be available, or you may already have a copy borrowed.");
            // }
        }

        private void ReturnMovie(Member member)
        {
            // Console.Write("Enter the Title of the Movie to return: ");
            // string title = Console.ReadLine();
            // if (member.ReturnMovie(title))
            // {
            //     movies.FindMovie(title)?.IncrementCopies();
            //     Console.WriteLine("Movie returned successfully.");
            // }
            // else
            // {
            //     Console.WriteLine("You did not borrow this movie.");
            // }
        }

        private void DisplayBorrowedMovies(Member member)
        {
            Console.WriteLine("Your borrowed movies:");
            foreach (var movie in member.BorrowedMovies)
            {
                Console.WriteLine($"{movie.Title} - Borrowed");
            }
        }

        private void DisplayTopRentedMovies()
        {
            // var topMovies = movies.GetTopRentedMovies(3);
            // Console.WriteLine("Top 3 Rented Movies:");
            // foreach (var movie in topMovies)
            // {
            //     Console.WriteLine($"{movie.Title} - {movie.TimesRented} times rented");
            // }
        }

    }
}
