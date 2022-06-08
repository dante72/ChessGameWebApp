﻿using ChessGame;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ChessWebAPI
{
    public class ServerWebApi
    {
        private readonly HttpClient _httpClient;
        public ServerWebApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new NullReferenceException(nameof(httpClient));
        }

        public async Task<ChessBoard?> GetBoard2()
        {
            var data = await _httpClient.GetFromJsonAsync<ChessBoardDto>("chessgame/board2");
            return data.FromDto();
        }

        public async Task<ChessCellDto?> GetCell()
        {
            return await _httpClient.GetFromJsonAsync<ChessCellDto>("chessgame/Cell");
        }

        public async Task<bool> TryMove(Cell from, Cell to)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"chessgame/move?fromRow={from.Row}&fromColumn={from.Column}&toRow={to.Row}&toColumn={to.Column}");
        }
        public async Task<FigureDto> GetFigure()
        {
            var ddd = await _httpClient.GetFromJsonAsync<FigureDto?>("chessgame/Figure");
            return ddd;
        }

        public async Task GetBoard(ChessBoard board)
        {
            var data = await _httpClient.GetFromJsonAsync<ChessBoardDto>("chessgame/board");
            board.Update(data);

            return;
        }
        public async Task Click(int row, int column, ChessBoard chessBoard)
        {
            //var data = await _httpClient.GetFromJsonAsync<ChessBoardDto>($"chessgame/click?row={row}&column={column}");
            chessBoard.Click(row, column);
            //chessBoard.Update(data ?? throw new NullReferenceException(nameof(data)));
        }
    }
}