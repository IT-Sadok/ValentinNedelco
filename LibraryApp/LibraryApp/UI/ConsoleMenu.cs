using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI
{
    public class ConsoleMenu
    {
        private readonly ILibraryService _libraryService;

        public ConsoleMenu(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public void Run()
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
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        private void AddBook()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter author: ");
            string author = Console.ReadLine() ?? "";

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
            string code = Console.ReadLine() ?? "";

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
            string code = Console.ReadLine() ?? "";

            bool isRemoved = _libraryService.RemoveBook(code);

            Console.WriteLine(isRemoved
                ? "Book removed successfully."
                : "Book not found.");
        }

        private void SearchBooks()
        {
            Console.Write("Enter title or author: ");
            string searchParameter = Console.ReadLine() ?? "";

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
            string code = Console.ReadLine() ?? "";

            bool isBorrowed = _libraryService.BorrowBook(code);

            Console.WriteLine(isBorrowed
                ? "Book borrowed successfully."
                : "Book not found or already borrowed.");
        }

        private void ReturnBook()
        {
            Console.Write("Enter book code: ");
            string code = Console.ReadLine() ?? "";

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
    }
}
