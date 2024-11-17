using Sudoku.Core;

namespace Sudoku.WebApp.Components;

public partial class SudokuGridComponent : IDisposable
{
    protected override void OnInitialized()
    {
        base.OnInitialized();
        GridData.GridChanged += OnGridChanged;
        ViewSettings.ViewSettingChanged += OnViewSettingsChanged;
    }

    public void Dispose()
    {
        GridData.GridChanged -= OnGridChanged;
        ViewSettings.ViewSettingChanged -= OnViewSettingsChanged;
    }

    public void OnGridChanged(object sender, EventArgs e)
    {
        StateHasChanged();
    }

    public void OnViewSettingsChanged(object sender, EventArgs e)
    {
        StateHasChanged();
    }

    private string CellClass(int row, int col)
    {
        var className = "sudoku-cell";

        if (row % 3 == 0)
            className += " sudoku-cell-top-thick";
        else
            className += " sudoku-cell-top-thin";
        if (row == 8)
            className += " sudoku-cell-bottom-thick";

        if (col % 3 == 0)
            className += " sudoku-cell-left-thick";
        else
            className += " sudoku-cell-left-thin";
        if (col == 8)
            className += " sudoku-cell-right-thick";

        return className;
    }

    private string TextClass(SudokuCell cell)
    {
        if (cell.CellType == CellType.Set)
            return "sudoku-text-set";

        if (ViewSettings.DarkMode)
            return "sudoku-text-editable-dark-mode";
        else
            return "sudoku-text-editable-light-mode";
    }
}
