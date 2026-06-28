using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI
{
    public class ConsoleMenu
    {
        private readonly ILibraryService _libraryService;
        private readonly IBookSimulationService _simulationService;

        public ConsoleMenu(
            ILibraryService libraryService,
            IBookSimulationService simulationService)
        {
            _libraryService = libraryService;
            _simulationService = simulationService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                DisplayMenu();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;

                    case "2":
                        RemoveBook();
                        break;

                    case "3":
                        SearchBooks();
                        break;

                    case "4":
                        DisplayAllBooks();
                        break;

                    case "5":
                        BorrowBook();
                        break;

                    case "6":
                        ReturnBook();
                        break;

                    case "7":
                        await RunSimulationAsync();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\n=== Library Menu ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Show All Books");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. Run 100 Tasks Simulation");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        private void AddBook()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter author: ");
            string author = Console.ReadLine() ?? string.Empty;

            int yearPublished;

            while (true)
            {
                Console.Write("Enter publication year: ");

                if (int.TryParse(Console.ReadLine(), out yearPublished)
                    && yearPublished > 0
                    && yearPublished <= DateTime.Now.Year)
                {
                    break;
                }

                Console.WriteLine("Invalid year.");
            }

            Console.Write("Enter unique code: ");
            string code = Console.ReadLine() ?? string.Empty;

            Book book = new Book
            {
                Title = title,
                Author = author,
                YearPublished = yearPublished,
                Code = code
            };

            bool isAdded = _libraryService.AddBook(book);

            Console.WriteLine(isAdded
                ? "Book added successfully."
                : "Book with this code already exists.");
        }

        private void RemoveBook()
        {
            Console.Write("Enter book code: ");
            string code = Console.ReadLine() ?? string.Empty;

            bool isRemoved = _libraryService.RemoveBook(code);

            Console.WriteLine(isRemoved
                ? "Book removed successfully."
                : "Book not found.");
        }

        private void SearchBooks()
        {
            Console.Write("Enter title or author: ");
            string searchParameter = Console.ReadLine() ?? string.Empty;

            List<Book> books = _libraryService.SearchBooks(searchParameter);

            DisplayBooks(books);
        }

        private void DisplayAllBooks()
        {
            List<Book> books = _libraryService.GetAllBooks();

            DisplayBooks(books);
        }

        private void BorrowBook()
        {
            Console.Write("Enter book code: ");
            string code = Console.ReadLine() ?? string.Empty;

            bool isBorrowed = _libraryService.BorrowBook(code);

            Console.WriteLine(isBorrowed
                ? "Book borrowed successfully."
                : "Book not found or already borrowed.");
        }

        private void ReturnBook()
        {
            Console.Write("Enter book code: ");
            string code = Console.ReadLine() ?? string.Empty;

            bool isReturned = _libraryService.ReturnBook(code);

            Console.WriteLine(isReturned
                ? "Book returned successfully."
                : "Book not found or already available.");
        }

        private void DisplayBooks(List<Book> books)
        {
            if (!books.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (Book book in books)
            {
                Console.WriteLine(
                    $"[{book.Code}] {book.Title} - {book.Author} ({book.YearPublished}) | Status: {book.Status}");
            }
        }

        private async Task RunSimulationAsync()
        {
            await _simulationService.RunSimulationAsync();
        }
    }
}
