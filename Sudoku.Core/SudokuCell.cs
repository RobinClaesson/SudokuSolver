using System.Collections.Immutable;

namespace Sudoku.Core;

/// <summary>
/// Represents a cell in a Sudoku grid.
/// </summary>
public class SudokuCell
{
    /// <summary>
    /// Creates an empty cell
    /// </summary>
    public SudokuCell() { }

    /// <summary>
    /// Creates a cell with the given value.
    /// </summary>
    /// <param name="value">Initial value of the cell.</param>
    public SudokuCell(int value) => _value = value;

    /// <summary>
    /// Vale of the cell. Can set value if editable. Accepted values: 0-9 (inclusive), 0 means empty. Setting the value will clear the candidates.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (CellType == CellType.Set)
                throw new InvalidOperationException("Cannot change the value of a Set cell.");

            if (value < 0 || value > 9)
                throw new ArgumentOutOfRangeException("Value", "Cell Value must be between 0 and 9 (inclusive).");

            _candidates.Clear();
            _value = value;
        }
    }
    private int _value = 0;

    public CellType CellType { get; set; } = CellType.Editable;

    /// <summary>
    /// Candiate values for an empty cell.
    /// </summary>
    public ImmutableHashSet<int> Candidates => _candidates.ToImmutableHashSet();
    private HashSet<int> _candidates = new();

    /// <summary>
    /// Adds a candidate value to the cell if editable. 
    /// </summary>
    /// <param name="candidate">Accepted values: 1-9 (inclusive)</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AddCandidate(int candidate)
    {
        if (CellType == CellType.Set)
            throw new InvalidOperationException("Cannot add canditate value of a Set cell.");

        if (candidate < 1 || candidate > 9)
            throw new ArgumentOutOfRangeException("Candidate", "Candidate must be between 1 and 9 (inclusive).");

        _candidates.Add(candidate);
    }

    /// <summary>
    /// Adds multiple candidate values to the cell if editable. 
    /// </summary>
    /// <param name="candidates">Accepted values: 1-9 (inclusive)</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AddCandidates(IEnumerable<int> candidates)
    {
        if (CellType == CellType.Set)
            throw new InvalidOperationException("Cannot add canditate value of a Set cell.");

        if (candidates.Any(c => c < 1) || candidates.Any(c => c > 9))
            throw new ArgumentOutOfRangeException("Candidate", "Candidate must be between 1 and 9 (inclusive).");

        foreach (var candidate in candidates)
            AddCandidate(candidate);
    }

    /// <summary>
    /// Sets the candidate values of a editable cell to a collection.
    /// </summary>
    /// <param name="candidates">Accepted values: 1-9 (inclusive)</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetCandidates(IEnumerable<int> candidates)
    {
        if (CellType == CellType.Set)
            throw new InvalidOperationException("Cannot add canditate value of a Set cell.");

        if (candidates.Any(c => c < 1) || candidates.Any(c => c > 9))
            throw new ArgumentOutOfRangeException("Candidate", "Candidate must be between 1 and 9 (inclusive).");

        _candidates = candidates.ToHashSet();
    }

    /// <summary>
    /// Removes a candidate value from the cell if editable.
    /// </summary>
    /// <param name="candidate"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void RemoveCandidate(int candidate)
    {
        if (CellType == CellType.Set)
            throw new InvalidOperationException("Cannot remove canditate value of a Set cell.");

        _candidates.Remove(candidate);
    }

    /// <summary>
    /// Clears the candiate values of the cell.
    /// </summary>
    public void ClearCanditates()
    {
        _candidates.Clear();
    }

    /// <summary>
    /// All the cells in the same Row as this cell in the grid.
    /// </summary>
    public List<SudokuCell> Row { get; internal set; } = new List<SudokuCell>();

    /// <summary>
    /// All the cells in the same Column as this cell in the grid.
    /// </summary>
    public List<SudokuCell> Column { get; internal set; } = new List<SudokuCell>();

    /// <summary>
    /// All the cells in the same Block as this cell in the grid.
    /// </summary>
    public List<SudokuCell> Block { get; internal set; } = new List<SudokuCell>();

    /// <summary>
    /// Checks if the cell is valid.
    /// </summary>
    /// <returns>True if the cell is empty (Value 0) or the value is unique in the row, column and block. False otherwise.</returns>
    public bool IsValid()
    {
        /// Empty cells are always valid
        if (Value == 0)
            return true;

        /// Check if the value is unique in the row, column, and block
        if (Row.Count(c => c.Value == this.Value) > 1 ||
            Column.Count(c => c.Value == this.Value) > 1 ||
            Block.Count(c => c.Value == this.Value) > 1)
            return false;

        // No conflicts found
        return true;
    }

    /// <summary>
    /// Check if the cell has the given number in its value or candidates.
    /// </summary>
    /// <param name="number">Number to check</param>
    /// <returns>true if cell has the number as value or in its candidates, false otherwise. </returns>
    public bool HasNumber(int number) => Value == number || Candidates.Contains(number);
}

/// <summary>
/// Represents the type of a cell in a Sudoku grid.
/// </summary>
public enum CellType
{
    Editable,
    Set,
}
