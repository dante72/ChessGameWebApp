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

        public bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            try
            {
                var from = _board.GetCell(fromRow, fromColumn);
                var to = _board.GetCell(toRow, toColumn);
                from.Figure.TryMoveTo(to);

                return true;
            }

            catch
            {
                return false;
            }

        }
    }
}
