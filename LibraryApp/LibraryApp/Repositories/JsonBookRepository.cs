using LibraryApp.Models;
using System.Text.Json;

namespace LibraryApp.Repositories
{
    public class JsonBookRepository : IBookRepository
    {
        private readonly string _filePath;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public JsonBookRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Book>();
            }

            var jsonData = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<Book>();
            }

            return JsonSerializer.Deserialize<List<Book>>(jsonData, _jsonOptions)
                   ?? new List<Book>();
        }

        public void SaveAllBooks(List<Book> books)
        {
            var jsonData = JsonSerializer.Serialize(books, _jsonOptions);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
