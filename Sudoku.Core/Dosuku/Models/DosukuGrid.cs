using System.Text.Json.Serialization;

namespace Sudoku.Core.Dosuku.Models;

public class DosukuGrid
{
    [JsonPropertyName("value")]
    public int[][] Value { get; set; } = new int[0][];

    [JsonPropertyName("solution")]
    public int[][] Solution { get; set; } = new int[0][];

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;
}
