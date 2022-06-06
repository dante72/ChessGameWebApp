using ChessGame;
using System.Text.Json;

namespace ChessGameWebApp.Server.Services
{
    internal class ServerGameService : IServerGameService
    {
        private readonly ILogger<ServerGameService> _logger;
        private readonly ChessBoard _board;
        public ServerGameService(ILogger<ServerGameService> logger, ChessBoard board)
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
