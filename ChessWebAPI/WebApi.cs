using ChessGame;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ChessWebAPI
{
    public class WebApi
    {
        private readonly HttpClient _httpClient;
        public WebApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new NullReferenceException(nameof(httpClient));
        }

        public async Task<List<Cell>> GetPossibleMovesAsync(int row, int column)
        {
            return (await _httpClient.GetFromJsonAsync<Cell[]>($"chessgame/possible_moves?row={row}&column={column}")).ToList();
        }

        public async Task<ChessBoard> GetBoard()
        {
            var b = await _httpClient.GetFromJsonAsync<ChessBoard>("chessgame/board");

            using (FileStream fs = new FileStream("board2.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, b);
            }
            return b;
        }
    }
}