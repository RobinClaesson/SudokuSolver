using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Xml.Serialization;

namespace Sudoku.Core;

/// <summary>
/// Represent a Sudoku grid with solving capabilities.
/// </summary>
public class SolvingSudokuGrid : SudokuGrid
{
    /// <summary>
    /// Invoked when the values of the grid are updated.
    /// </summary>
    public EventHandler GridUpdated;

    private List<List<SudokuCell>> _rows = new();
    private List<List<SudokuCell>> _columns = new();
    private List<List<SudokuCell>> _blocks = new();

    private readonly IEnumerable<int> _allValues = Enumerable.Range(1, 9);


    /// <summary>
    /// Creates a new self solving Sudoku grid with empty cells.
    /// </summary>
    public SolvingSudokuGrid() : base()
    {
        SetRowsColumnsAndBlocks();
    }

    /// <summary>
    /// Creates a new self solving Sudoku grid with the given set values.
    /// </summary>
    /// <param name="grid">Initial values for the grid.</param>
    public SolvingSudokuGrid(int[,] grid) : base(grid)
    {
        SetRowsColumnsAndBlocks();
    }

    /// <summary>
    /// Creates a new self solving Sudoku grid with the given set values.
    /// </summary>
    /// <param name="grid">Initial values for the grid.</param>
    public SolvingSudokuGrid(int[][] grid) : base(grid)
    {
        SetRowsColumnsAndBlocks();
    }

    /// <summary>
    /// Parses a string into a SolvingSudokuGrid object.
    /// </summary>
    /// <param name="input">9 lines of 9 numbers 0-9, 0 indicates empy cell</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static SolvingSudokuGrid FromString(string input)
    {
        return (SolvingSudokuGrid)SudokuGrid.FromString(input);
    }

    /// <summary>
    /// Collects all rows, columns and blocks in the grid and fills the private variables.
    /// </summary>
    private void SetRowsColumnsAndBlocks()
    {
        _rows.Clear();
        _columns.Clear();
        _blocks.Clear();
        foreach (var cell in _grid)
        {
            _rows.Add(cell.Row);
            _columns.Add(cell.Column);
            _blocks.Add(cell.Block);
        }
        _rows = _rows.Distinct().ToList();
        _columns = _columns.Distinct().ToList();
        _blocks = _blocks.Distinct().ToList();
    }

    /// <summary>
    /// Fill the candidates list for all empty cells in the grid.
    /// </summary>
    public void CalculateCandidates()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                CalculateCandidates(_grid[row, column]);
            }
        }
    }

    /// <summary>
    /// Fills the candidates list with all possible values for the cell.
    /// </summary>
    public void CalculateCandidates(SudokuCell cell)
    {
        if (cell.Value != 0)
            return;

        var rowValues = cell.Row.Where(c => c.Value != 0).Select(c => c.Value);
        var columnValues = cell.Column.Where(c => c.Value != 0).Select(c => c.Value);
        var blockValues = cell.Block.Where(c => c.Value != 0).Select(c => c.Value);

        cell.ClearCanditates();
        for (int i = 1; i <= 9; i++)
        {
            if (!rowValues.Contains(i) && !columnValues.Contains(i) && !blockValues.Contains(i))
                cell.AddCandidate(i);
        }
    }

    /// <summary>
    /// Solves the Sudoku grid.
    /// First uses logical solving methods and then brute force if needed.
    /// </summary>
    /// <returns>true if the sudoku is solved, false otherwise.</returns>
    public bool Solve()
    {
        //Logical solve
        var changed = new List<bool>();
        do
        {
            changed.Clear();

            // Candidate calculations
            CalculateCandidates();
            SolveObviousPairs();

            // Value Calculations
            changed.Add(SolveObviousSingles());
            changed.Add(SolveHiddenSingles());
            changed.Add(SolveLastFreeCell());
            changed.Add(SolveLastPossibleNumber());

            GridUpdated?.Invoke(this, EventArgs.Empty);

        } while (changed.Any(b => b));

        if (IsSolved())
            return true;

        // Brute force
        if (IsValid())
            return SolveBruteForce();

        return false;
    }

    /// <summary>
    /// Finds empty cells with only one candidate and sets the value.
    /// </summary>
    /// <returns>true if any values were set, false otherwise.</returns>
    public bool SolveObviousSingles()
    {
        var changed = false;
        foreach (var cell in _grid)
        {
            if (cell.Value == 0 && cell.Candidates.Count == 1)
            {
                changed = true;
                cell.Value = cell.Candidates.First();
            }
        }

        return changed;
    }

    /// <summary>
    /// Finds empty cells with a unique candidate in the row, column or block and sets the value.
    /// </summary>
    /// <returns>true if any values are set, false otherwise.</returns>
    public bool SolveHiddenSingles()
    {
        var changed = false;
        foreach (var cell in _grid)
        {
            if (cell.Value == 0)
            {
                var value = cell.Candidates.FirstOrDefault(c => cell.Row.All(r => r == cell || !r.HasNumber(c))
                                                            || cell.Column.All(r => r == cell || !r.HasNumber(c))
                                                            || cell.Block.All(r => r == cell || !r.HasNumber(c)));

                if (value != 0)
                {
                    changed = true;
                    cell.Value = value;
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// Finds rows, columns and blocks with only one empty cell and sets the value.
    /// </summary>
    /// <returns>true if any values are set, false otherwise.</returns>
    public bool SolveLastFreeCell()
    {
        var changed = false;

        foreach (var row in _rows)
            changed = changed || FindAndSetMissingValues(row);

        foreach (var row in _columns)
            changed = changed || FindAndSetMissingValues(row);

        foreach (var row in _blocks)
            changed = changed || FindAndSetMissingValues(row);

        return changed;
    }

    /// <summary>
    /// Finds cells where all other values are already in the row, column or block and sets the value.
    /// </summary>
    /// <returns>true if any values are set, false otherwise.</returns>
    public bool SolveLastPossibleNumber()
    {
        var changed = false;

        foreach (var cell in _grid)
        {
            if (cell.Value == 0)
            {
                var rowValues = cell.Row.Where(c => c.Value != 0).Select(c => c.Value);
                var columnValues = cell.Column.Where(c => c.Value != 0).Select(c => c.Value);
                var blockValues = cell.Block.Where(c => c.Value != 0).Select(c => c.Value);

                var conflictingValues = rowValues.Concat(columnValues).Concat(blockValues).Distinct();
                var missingValues = _allValues.Except(conflictingValues).ToList();

                if (missingValues.Count() == 1)
                {
                    cell.Value = missingValues.First();
                    changed = true;
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// Checks if the collection only contains one missing value and sets it.
    /// </summary>
    /// <param name="cells">Collection of cells to check</param>
    /// <returns>true if any values are set, false otherwise.</returns>
    private bool FindAndSetMissingValues(List<SudokuCell> cells)
    {
        if (cells.Count(c => c.Value == 0) == 1)
        {
            var values = cells.Select(c => c.Value).Where(v => v != 0);
            var missing = _allValues.Except(values).First();
            cells.First(c => c.Value == 0).Value = missing;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Finds pairs of empty cells in a block with only the same two candidates and removes the candidates from other cells in the block.
    /// </summary>
    public void SolveObviousPairs()
    {
        foreach (var block in _blocks.Where(b => b.Count(c => c.Value == 0) > 2))
        {
            //  Cells that have two candidates
            var hasTwoCandidates = block.Where(c => c.Value == 0 && c.Candidates.Count == 2);

            //  Group cells with the same candidates
            var groups = hasTwoCandidates.GroupBy(c => string.Join('.', c.Candidates));

            foreach (var pair in groups.Where(g => g.Count() >= 2))
            {
                var candidates = pair.First().Candidates.ToList();
                var otherToChange = block.Except(pair).Where(c => c.CellType == CellType.Editable);

                foreach (var cell in otherToChange)
                {
                    cell.RemoveCandidate(candidates[0]);
                    cell.RemoveCandidate(candidates[1]);
                }

            }
        }
    }

    /// <summary>
    /// Uses a depth first search with the rules of Solve() to brute force the solution.
    /// </summary>
    /// <returns>True if a valid solution is found from the current position, false otherwise</returns>
    public bool SolveBruteForce()
    {
        var currentGrid = ExportGrid();
        var currentCandidates = ExportCandidates();

        var sortedEmptyCells = _grid.Cast<SudokuCell>()
                                    .Where(c => c.Value == 0)
                                    .OrderBy(c => c.Candidates.Count)
                                    .ToList();

        foreach (var cell in sortedEmptyCells)
        {
            foreach (var candidate in cell.Candidates)
            {
                cell.Value = candidate;

                var result = Solve();

                if (result)
                    return true;

                ImportGrid(currentGrid);
                ImportCandidates(currentCandidates);
            }
        }

        return false;
    }
}
