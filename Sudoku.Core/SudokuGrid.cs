using Sudoku.Core.Util;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Sudoku.Core;

/// <summary>
/// Represents a Sudoku grid
/// </summary>
public class SudokuGrid
{
    protected readonly SudokuCell[,] _grid = new SudokuCell[9, 9];

    /// <summary>
    /// Get the cell at the given row and column.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns>Cell object at given coardinates</returns>
    public SudokuCell this[int row, int column]
    {
        get => _grid[row, column];
    }

    /// <summary>
    /// Creates a new Sudoku grid with empty cells.
    /// </summary>
    public SudokuGrid()
    {
        InitCells(new int[9, 9]);
    }

    /// <summary>
    /// Creates a new Sudoku grid with the given set values.
    /// </summary>
    /// <param name="grid">Initial values for the grid.</param>
    public SudokuGrid(int[,] grid)
    {
        InitCells(grid);
    }

    /// <summary>
    /// Creates a new Sudoku grid with the given set values.
    /// </summary>
    /// <param name="grid">Initial values for the grid.</param>
    public SudokuGrid(int[][] grid)
    {
        InitCells(ArrayUtil.To2D(grid));
    }

    /// <summary>
    /// Parses a string into a SudokuGrid object.
    /// </summary>
    /// <param name="input">9 lines of 9 numbers 0-9, 0 indicates empy cell</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static SudokuGrid FromString(string input)
    {
        var rows = input.Trim().Replace("\r", "").Split("\n");

        if (rows.Length != 9)
            throw new ArgumentException($"Invalid input! Expected 9 rows got {rows.Length} rows!");

        if (rows.Any(rows => rows.Length != 9))
        {
            var missing = rows.First(rows => rows.Length != 9);
            var index = Array.IndexOf(rows, missing);
            throw new ArgumentException($"Invalid input! Expected all rows to have 9 characters. Row {index + 1} has {missing.Length} characters!");
        }

        var chars = rows.Select(row => row.ToCharArray()).ToArray();
        if (chars.Any(row => row.Any(c => c < '0' || c > '9')))
        {
            var invalid = chars.First(row => row.Any(c => c < '0' || c > '9'));
            var rowIndex = Array.IndexOf(chars, invalid);
            var colIndex = Array.IndexOf(invalid, invalid.First(c => c < '0' || c > '9'));
            throw new ArgumentException($"Invalid input! Expected all characters to be between 0 and 9. Found {invalid[colIndex]} at row {rowIndex + 1} column {colIndex + 1}");
        }

        var grid = chars.Select(row => row.Select(c => c - '0').ToArray()).ToArray();

        return new SolvingSudokuGrid(grid);
    }

    /// <summary>
    /// Initializes the cells of the grid with the given values and internal references.
    /// </summary>
    /// <param name="grid">Intial values of th grid.</param>
    private void InitCells(int[,] grid)
    {
        // Initialize the grid with empty cells
        // Initialize the row references
        for (int row = 0; row < 9; row++)
        {
            var rowList = new List<SudokuCell>();
            for (int column = 0; column < 9; column++)
            {
                _grid[row, column] = new SudokuCell(grid[row, column]);
                if (grid[row, column] != 0)
                    _grid[row, column].CellType = CellType.Set;

                rowList.Add(_grid[row, column]);
                _grid[row, column].Row = rowList;
            }
        }

        // Initialize the column references
        for (int column = 0; column < 9; column++)
        {
            var columnList = new List<SudokuCell>();
            for (int row = 0; row < 9; row++)
            {
                columnList.Add(_grid[row, column]);
                _grid[row, column].Column = columnList;
            }
        }

        // Initialize the block references
        for (int blockRow = 0; blockRow < 3; blockRow++)
        {
            for (int blockColumn = 0; blockColumn < 3; blockColumn++)
            {
                var blockList = new List<SudokuCell>();
                for (int row = blockRow * 3; row < blockRow * 3 + 3; row++)
                {
                    for (int column = blockColumn * 3; column < blockColumn * 3 + 3; column++)
                    {
                        blockList.Add(_grid[row, column]);
                        _grid[row, column].Block = blockList;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Checks if the grid is valid.
    /// </summary>
    /// <returns>True if the grid contains no duplicate numbers in any row, column or block. False otherwise.</returns>
    public bool IsValid() => _grid.Cast<SudokuCell>().All(c => c.IsValid());

    /// <summary>
    /// Checks if the grid is solved.
    /// </summary>
    /// <returns>true if the sudoku is solved,false otherwise.</returns>
    public bool IsSolved() => _grid.Cast<SudokuCell>().All(c => c.Value != 0 && c.IsValid());


    /// <summary>
    /// Returns a boolean matrix indicating which cells are valid.
    /// </summary>
    /// <returns>A 9x9 matrix with true for cells that are valid and false for cells that are invalid.</returns>
    public bool[,] ValidCells()
    {
        var validCells = new bool[9, 9];
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                validCells[row, column] = _grid[row, column].IsValid();
            }
        }
        return validCells;
    }

    /// <summary>
    /// Exports the values of the grid.
    /// </summary>
    /// <returns>9x9 2D array of the grid values.</returns>
    public int[,] ExportGrid()
    {
        var grid = new int[9, 9];
        for (int row = 0; row < 9; row++)
            for (int column = 0; column < 9; column++)
                grid[row, column] = _grid[row, column].Value;

        return grid;
    }

    /// <summary>
    /// Exports the candidates of the grid.
    /// </summary>
    /// <returns>9x9 2D array of the grid candidates as ImmutableHashSets</returns>
    public ImmutableHashSet<int>[,] ExportCandidates()
    {
        var grid = new ImmutableHashSet<int>[9, 9];
        for (int row = 0; row < 9; row++)
            for (int column = 0; column < 9; column++)
                grid[row, column] = _grid[row, column].Candidates;

        return grid;
    }

    /// <summary>
    /// Imports the values of the grid. Only sets the values of cells that are editable.
    /// </summary>
    /// <param name="grid">9x9 2D array of values 0-9 (inclusive)</param>
    public void ImportGrid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
            for (int column = 0; column < 9; column++)
                if (_grid[row, column].CellType != CellType.Set)
                    _grid[row, column].Value = grid[row, column];
    }

    /// <summary>
    /// Imports the candidates of the grid. Only sets the candidates of cells that are editable.
    /// </summary>
    /// <param name="candidates">9x9 2D array with collections of values 1-9 (inclusive)</param>
    public void ImportCandidates(IEnumerable<int>[,] candidates)
    {
        for (int row = 0; row < 9; row++)
            for (int column = 0; column < 9; column++)
                if (_grid[row, column].CellType != CellType.Set)
                    _grid[row, column].SetCandidates(candidates[row, column]);
    }

    /// <summary>
    /// Resets the grid to its initial state.
    /// </summary>
    public void Reset()
    {
        for (int row = 0; row < 9; row++)
            for (int column = 0; column < 9; column++)
                if (_grid[row, column].CellType != CellType.Set)
                    _grid[row, column].Value = 0;
    }

}
