namespace LibraryApp.Models
{
    public class Book
    {
        public string Code { get; set; } = "";
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int YearPublished { get; set; }
        public BookStatus Status { get; set; } = BookStatus.Available;
    }
}
