using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChessGameWebApp.Server.SignalRHub
{
    public class GameHub : Hub
    {

        private readonly ILogger<GameHub> _logger;
        private readonly ChessBoard _board;
        private readonly IServerGameService _serverGameService;
        public GameHub(ILogger<GameHub> logger, ChessBoard board, IServerGameService serverGameService)
        {
            _logger = logger;
            _board = board;
            _serverGameService = serverGameService;
        }
        public async Task SendBoard()
        {
            await Clients.Caller.SendAsync("ReceiveBoard", _board.ToDto());
        }

        public async Task SendTryMove(ChessCellDto from, ChessCellDto to)
        {
            _serverGameService.TryMove(from.Row, from.Column, to.Row, to.Column);
            await Clients.All.SendAsync("ReceiveTryMove", from, to);
        }
    }
}
