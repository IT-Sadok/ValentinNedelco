namespace LibraryApp.Models
{
    public class Book
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int YearPublished { get; set; }
        public BookStatus Status { get; set; } = BookStatus.Available;
    }
}
