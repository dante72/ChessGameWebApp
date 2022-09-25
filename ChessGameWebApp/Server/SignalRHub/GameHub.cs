using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Player = ChessGameWebApp.Server.Models.Player;

namespace ChessGameWebApp.Server.SignalRHub
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {

        private readonly ILogger<GameHub> _logger;
        private readonly IGameSessionService _serverGameService;
        private readonly IConnectionService _connectionService;
        private readonly IPlayerService _playerService;

        public GameHub(ILogger<GameHub> logger, IGameSessionService serverGameService, IConnectionService connectionService, IPlayerService playerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serverGameService = serverGameService ?? throw new ArgumentNullException(nameof(serverGameService));
            _connectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }
        public async Task SendBoard()
        {
            int accountId = GetCurrentAccountId(Context);
            var session = await _serverGameService.GetSession(accountId);
            var player = session.GetPlayer(accountId);
            await Clients.Caller.SendAsync("ReceiveBoard", session.Board.ToDto(), player.Color, session.Board.GetTimer());
        }

        public async Task SendTryMove(ChessCellDto from, ChessCellDto to)
        {
            int accountId = GetCurrentAccountId(Context);
            var session = await _serverGameService.GetSession(accountId);

            try
            {
                if (session.IsAllowedMove(accountId))
                {
                    TryMove(session.Board, from.Row, from.Column, to.Row, to.Column);
                }
                else
                    throw new InvalidOperationException("It's not his move");

                if (session.Board.GameStatus == GameStatus.Checkmate || session.Board.GameStatus == GameStatus.Stalemate || session.Board.GameStatus == GameStatus.TimeIsUp)
                    await _serverGameService.CloseSession(accountId);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogInformation(ex.Message);
                from = null;
                to = null;
            }
            finally
            {
                var connections = await _connectionService.GetConnections(session.Players.Select(p => p.Id).ToArray());
                await Clients.Clients(connections).SendAsync("ReceiveTryMove", from, to, session.Board.GetTimer());
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task MoveBack()
        {
            int accountId = GetCurrentAccountId(Context);
            var session = await _serverGameService.GetSession(accountId);
            session.Board.TryMoveBack();

            var connections = await _connectionService.GetConnections(session.Players.Select(p => p.Id).ToArray());
            await Clients.Clients(connections).SendAsync("ReceiveMoveBack", true);
        }

        public async Task GameOver()
        {
            int accountId = GetCurrentAccountId(Context);
            var session = await _serverGameService.GetSession(accountId);

            if (session.Board.СhessСlock.IsGameOver())
                await _serverGameService.CloseSession(accountId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _connectionService.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async override Task OnConnectedAsync()
        {
            //обязательная авторизация до соединения
            int id = GetCurrentAccountId(Context);
                
            await _connectionService.Add(id, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task StartGame()
        {
            await Clients.Caller.SendAsync("StartGame", false);
        }

        private int GetCurrentAccountId(HubCallerContext context)
        {
            try
            {
                return int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
                return -1;
            }
        }

        private bool TryMove(Board board, int fromRow, int fromColumn, int toRow, int toColumn)
        {
            try
            {
                var from = board.GetCell(fromRow, fromColumn);
                var to = board.GetCell(toRow, toColumn);
                board.TryMove(from, to);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task AddOrRemovePlayer(int rivalId)
        {
            int id = GetCurrentAccountId(Context);
            Player player = new Player() { Id = id, RivalId = rivalId };
            var result = await _playerService.AddOrRemove(player);

            var connections = await _connectionService.GetConnections(new int[] { id, rivalId });
            await Clients.Clients(connections).SendAsync("ChangeStatus", id, result);
        }
    }
}
