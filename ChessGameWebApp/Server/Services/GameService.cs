using ChessGame;
using System.Text.Json;

namespace ChessGameWebApp.Server.Services
{
    internal class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly ChessBoard _board;
        public GameService(ILogger<GameService> logger, ChessBoard board)
        {
            _logger = logger;
            _board = board;
        }
        public ChessBoard GetBoard()
        {
            _logger.LogInformation("Get Board");
            return _board;
        }
        public ChessBoard Click(int row, int column)
        {
            _board.Click(row, column);
            return _board;
        }
    }
}
