using Sudoku.Core;
using Sudoku.Core.Dosuku;
using Sudoku.Core.Dosuku.Models;

namespace Sudoku.WebApp;

public static class GridData
{
    public static EventHandler GridChanged;
    public static bool IsFetching { get; private set; }
    public static DosukuGrid DosukuGrid { get; set; } = new DosukuGrid();
    public static SolvingSudokuGrid SolvingSudokuGrid { get; set; } = new SolvingSudokuGrid();

    public static async Task FetchDosukuGrid()
    {
        IsFetching = true;
        var data = await DosukuHandler.FetchNewBoardAsync();
        if (data != null)
        {
            DosukuGrid = data.NewBoard.Grids[0];
            SolvingSudokuGrid = new SolvingSudokuGrid(DosukuGrid.Value);
            SolvingSudokuGrid.GridUpdated += (sender, e) => InvokeGridChanged();
            InvokeGridChanged();
        }
        IsFetching = false;
    }

    public static void InvokeGridChanged()
    {
        GridChanged?.Invoke(null, EventArgs.Empty);
    }
}
