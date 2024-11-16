using System.Text.Json.Serialization;

namespace Sudoku.Core.Dosuku.Models;

public class DosukuNewBoard
{
    [JsonPropertyName("grids")]
    public List<DosukuGrid> Grids { get; set; } = new();

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
