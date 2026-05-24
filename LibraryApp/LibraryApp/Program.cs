using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.UI;

IBookRepository bookRepository = new JsonBookRepository("books.json");

ILibraryService libraryService = new LibraryService(bookRepository);

ConsoleMenu consoleMenu = new ConsoleMenu(libraryService);

consoleMenu.Run();