using CommandLineMenu;
using Sudoku.Core;
using Sudoku.Core.Dosuku;
using System.Text;

var mainMenu = new Menu<string> { "Fetch puzzle from API",
                                  "Read puzle from file",
                                  "Enter puzzle",
                                  "Help",
                                  "Exit"};
switch (mainMenu.ShowMenu())
{
    default:
    case "Exit":
        Environment.Exit(0);
        break;

    case "Fetch puzzle from API":
        Console.WriteLine("Fecthing puzzle...");
        var dosukuResponse = await DosukuHandler.FetchNewBoardAsync();
        if (dosukuResponse == null)
        {
            Console.WriteLine("Failed to fetch puzzle!");
            Environment.Exit(1);
        }
        var apiGrid = new SolvingSudokuGrid(dosukuResponse.FirstGrid);
        SolveAndPrintGrid(apiGrid);

        break;

    case "Read puzle from file":
            Console.Write("Enter file path: ");
            var filePath = Console.ReadLine() ?? string.Empty;
            var fileGrid = GridFromFile(filePath);
            SolveAndPrintGrid(fileGrid);
        break;

    case "Enter puzzle":
        Console.WriteLine("Enter puzzle:");
        var stdInGrid = GridFromStdIn();
        SolveAndPrintGrid(stdInGrid);
        break;

    case "Help":
        PrintHelp();
        break;
}


void SolveAndPrintGrid(SolvingSudokuGrid grid)
{
    Console.WriteLine("Initial grid:");
    PrintGrid(grid);
    Console.WriteLine("Solving...");
    var solvedGrid = grid.Solve();
    Console.WriteLine("Solved grid:");
    PrintGrid(grid);
}

SolvingSudokuGrid GridFromStdIn()
{
    var sb = new StringBuilder();
    for (int i = 1; i <= 9; i++)
    {
        Console.Write($"Enter row {i}: ");
        sb.AppendLine(Console.ReadLine());
    }

    return SolvingSudokuGrid.FromString(sb.ToString());
}

SolvingSudokuGrid GridFromFile(string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.WriteLine($"File '{filePath}' not found!");
        Environment.Exit(1);
    }
    return SolvingSudokuGrid.FromString(File.ReadAllText(filePath));
}

void PrintGrid(SudokuGrid grid)
{
    Console.WriteLine("┌─────┬─────┬─────┐");
    for (int i = 0; i < 9; i++)
    {

        for (int j = 0; j < 9; j++)
        {
            if (j % 3 == 0)
                Console.Write("│");
            else
                Console.Write(" ");
            if (grid[i, j].Value == 0)
                Console.Write(" ");
            else
                Console.Write(grid[i, j].Value);
        }
        if (i == 2 || i == 5)
            Console.WriteLine("│\n├─────┼─────┼─────┤");
        else
            Console.WriteLine("│");
    }
    Console.WriteLine("└─────┴─────┴─────┘");
}

void PrintHelp()
{
    Console.WriteLine("This is a Sudoku solver.");
    Console.WriteLine("You can fetch a puzzle from an API, read a puzzle from a file or enter a puzzle manually.");
    Console.WriteLine("The puzzle will be solved and printed to the console.");
    Console.WriteLine("The puzzle should be entered as 9 lines of 9 numbers, 0 indicates an empty cell. Same for puzzles in files");
    Console.WriteLine("Example input:");
    Console.WriteLine("530070000\n600195000\n098000060\n800060003\n400803001\n700020006\n060000280\n000419005\n000080079");
    Console.ReadKey();
}