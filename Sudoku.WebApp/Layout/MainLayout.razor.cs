using MudBlazor;
using System.Text.Json;

namespace Sudoku.WebApp.Layout;

public partial class MainLayout
{
    string DarkModeIcon => ViewSettings.DarkMode ? Icons.Material.Filled.DarkMode : Icons.Material.Filled.LightMode;

    private async void FetchGrid()
    {
        await GridData.FetchDosukuGrid();
        StateHasChanged();
    }

    private void SolveGrid()
    {
        GridData.SolvingSudokuGrid.Solve();
    }

    private void ResetGrid()
    {
        GridData.SolvingSudokuGrid.Reset();
        GridData.InvokeGridChanged();
    }
}
