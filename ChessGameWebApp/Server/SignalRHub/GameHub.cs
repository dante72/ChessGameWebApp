using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChessGameWebApp.Server.SignalRHub
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {

        private readonly ILogger<GameHub> _logger;
        private readonly ChessBoard _board;
        private readonly IServerGameService _serverGameService;
        public GameHub(ILogger<GameHub> logger, ChessBoard board, IServerGameService serverGameService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _serverGameService = serverGameService ?? throw new ArgumentNullException(nameof(serverGameService));
        }
        public async Task SendBoard()
        {
            await Clients.Caller.SendAsync("ReceiveBoard", _board.ToDto());
        }

        public async Task SendTryMove(ChessCellDto from, ChessCellDto to)
        {
            try
            {
                _serverGameService.TryMove(from.Row, from.Column, to.Row, to.Column);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogInformation(ex.Message);
                from = null;
                to = null;
            }
            finally
            {
                await Clients.All.SendAsync("ReceiveTryMove", from, to);
            }
        }

        public async Task StartGame()
        {
            await Clients.Caller.SendAsync("StartGame", false);
        }
    }
}
