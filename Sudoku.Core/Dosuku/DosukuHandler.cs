using System.Net.Http.Json;
using Sudoku.Core.Dosuku.Models;

namespace Sudoku.Core.Dosuku;

public static class DosukuHandler
{
    private const string BASE_URL = "https://sudoku-api.vercel.app/api/dosuku";
    private static HttpClient _httpClient = new HttpClient();

    public static async Task<DosukuResponse?> FetchNewBoardAsync()
    {
        var response = await _httpClient.GetAsync(BASE_URL);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<DosukuResponse>();
    }
}
