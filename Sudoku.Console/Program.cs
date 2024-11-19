using Sudoku.Core;
using System.Text;

Console.WriteLine("Sudoku Solver!");
var grid = args.Length > 0 ? GridFromFile() : GridFromStdIn();

Console.WriteLine("Initial Grid:");
PrintGrid(grid);

Console.WriteLine("Solving....");
grid.Solve();

Console.WriteLine("Solved Grid:");
PrintGrid(grid);


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

SolvingSudokuGrid GridFromFile()
{
    if (!File.Exists(args[0]))
    {
        Console.WriteLine($"File '{args[0]}' not found!");
        Environment.Exit(1);
    }
    return SolvingSudokuGrid.FromString(File.ReadAllText(args[0]));
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