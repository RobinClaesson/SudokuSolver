namespace Sudoku.WebApp;

public static class ViewSettings
{
    public static EventHandler ViewSettingChanged;

    public static bool DarkMode { get; private set; } = true;

    public static void ToggleDarkMode()
    {
        DarkMode = !DarkMode;
        ViewSettingChanged?.Invoke(null, EventArgs.Empty);
    }
}
