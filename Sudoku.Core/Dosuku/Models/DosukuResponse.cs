using System.Text.Json.Serialization;

namespace Sudoku.Core.Dosuku.Models;

public class DosukuResponse
{
    [JsonPropertyName("newboard")]
    public DosukuNewBoard NewBoard { get; set; } = new();

    public int[][] FirstGrid => NewBoard.Grids[0].Value;
}
