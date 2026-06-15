using LibraryApp.Models;
using LibraryApp.Repositories;

namespace LibraryApp.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;

        private readonly object _lock = new();

        public LibraryService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool AddBook(Book book)
        {
            lock (_lock)
            {
                List<Book> books = _bookRepository.GetAllBooks();

                var existingBook = books.FirstOrDefault(b =>
                    b.Code.Equals(book.Code, StringComparison.OrdinalIgnoreCase));

                if (existingBook is not null)
                {
                    return false;
                }

                books.Add(book);
                _bookRepository.SaveAllBooks(books);

                return true;
            }
        }

        public bool RemoveBook(string code)
        {
            lock (_lock)
            {
                List<Book> books = _bookRepository.GetAllBooks();

                var book = books.FirstOrDefault(b =>
                    b.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

                if (book is null)
                {
                    return false;
                }

                books.Remove(book);
                _bookRepository.SaveAllBooks(books);

                return true;
            }
        }

        public List<Book> SearchBooks(string searchParameter)
        {
            lock (_lock)
            {
                List<Book> books = _bookRepository.GetAllBooks();

                return books
                    .Where(book =>
                        book.Title.Contains(searchParameter, StringComparison.OrdinalIgnoreCase) ||
                        book.Author.Contains(searchParameter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        public List<Book> GetAllBooks()
        {
            lock (_lock)
            {
                return _bookRepository
                    .GetAllBooks()
                    .OrderBy(book => book.Title)
                    .ToList();
            }
        }

        public bool BorrowBook(string code)
        {
            lock (_lock)
            {
                List<Book> books = _bookRepository.GetAllBooks();

                var book = books.FirstOrDefault(b =>
                    b.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

                if (book is null || book.Status == BookStatus.Borrowed)
                {
                    return false;
                }

                book.Status = BookStatus.Borrowed;

                _bookRepository.SaveAllBooks(books);

                return true;
            }
        }

        public bool ReturnBook(string code)
        {
            lock (_lock)
            {
                List<Book> books = _bookRepository.GetAllBooks();

                var book = books.FirstOrDefault(b =>
                    b.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

                if (book is null || book.Status == BookStatus.Available)
                {
                    return false;
                }

                book.Status = BookStatus.Available;

                _bookRepository.SaveAllBooks(books);

                return true;
            }
        }
    }
}
