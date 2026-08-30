using LibraryApp.Models;

namespace LibraryApp.Services
{
    public interface ILibraryService
    {
        bool AddBook(Book book);
        bool RemoveBook(string code);
        List<Book> SearchBooks(string searchParameter);
        List<Book> GetAllBooks();
        bool BorrowBook(string code);
        bool ReturnBook(string code);
    }
}
