using ChessGame;
using System.Net.Http.Json;
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

        public async Task<IEnumerable<Cell>> GetPossibleMovesAsync(int row, int column)
        {
            return (await _httpClient.GetFromJsonAsync<List<ChessCellDto>>($"chessgame/possible_moves?row={row}&column={column}")).Select(i => i.Map());
        }
    }
}