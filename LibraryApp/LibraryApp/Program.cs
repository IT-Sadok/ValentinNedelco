using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.UI;

IBookRepository bookRepository = new JsonBookRepository("books.json");

ILibraryService libraryService = new LibraryService(bookRepository);

IBookSimulationService simulationService = new BookSimulationService(bookRepository);

ConsoleMenu consoleMenu = new ConsoleMenu(
    libraryService,
    simulationService);

await consoleMenu.RunAsync();