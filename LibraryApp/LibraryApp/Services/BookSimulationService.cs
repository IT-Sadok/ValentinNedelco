using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class BookSimulationService : IBookSimulationService
{
    private readonly IBookRepository _bookRepository;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public BookSimulationService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task RunSimulationAsync()
    {
        var tasks = new Task[100];

        for (int i = 0; i < tasks.Length; i++)
        {
            int taskNumber = i;

            tasks[i] = Task.Run(async () =>
            {
                await EditBookDataAsync(taskNumber);
            });
        }

        await Task.WhenAll(tasks);

        Console.WriteLine("Simulation finished. 100 tasks modified book data.");
    }

    private async Task EditBookDataAsync(int taskNumber)
    {
        await _semaphore.WaitAsync();

        try
        {
            var books = _bookRepository.GetAllBooks();

            if (!books.Any())
            {
                Console.WriteLine($"Task {taskNumber}: no books found.");
                return;
            }

            //used to distribute tasks accross the books
            var book = books[taskNumber % books.Count];

            book.Title = $"Updated Title by Task {taskNumber}";
            book.Author = $"Updated Author by Task {taskNumber}";

            _bookRepository.SaveAllBooks(books);

            Console.WriteLine($"Task {taskNumber}: updated book with code {book.Code}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
