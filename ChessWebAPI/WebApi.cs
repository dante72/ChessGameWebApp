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

        public async Task Update(ChessBoard chessBoard)
        {
            var data =  await _httpClient.GetFromJsonAsync<ChessBoardDto>("chessgame/board");
            chessBoard.Update(data ?? throw new NullReferenceException(nameof(data)));
        }
    }
}