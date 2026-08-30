using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        void SaveAllBooks(List<Book> books);
    }
}
