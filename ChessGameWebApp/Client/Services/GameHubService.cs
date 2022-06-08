using ChessGame;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChessGameWebApp.Client.Services
{
    public class GameHubService : IGameHubService, IAsyncDisposable
    {
        private readonly ILogger<GameHubService> _logger;
        private readonly ChessBoard _board;
        private readonly NavigationManager _navigationManager;
        private HubConnection hubConnection;

        public ChessCell[] CurrentMove { get; set; } = new ChessCell[0];
        public GameHubService(ILogger<GameHubService> logger, ChessBoard board, NavigationManager navigationManager)
        {
            _logger = logger;
            _board = board;
            _navigationManager = navigationManager;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("/gamehub"))
                .Build();

            hubConnection.On<ChessBoardDto>("ReceiveBoard", (board) =>
            {
                _board.Update(board);
            });

            hubConnection.On<ChessCellDto, ChessCellDto>("ReceiveTryMove", (from, to) =>
            {

                    var f = _board.GetCell(from.Row, from.Column);
                    var t = _board.GetCell(to.Row, to.Column);

                    f.Figure?.TryMoveTo(t);
            });

            Task.Run(async () => { await hubConnection.StartAsync(); });
        }

        public async Task GetBoard()
        {
            if (IsConnected)
            {
                await hubConnection.SendAsync("SendBoard");
            }
        }

        public async Task SendTryMove(Cell from, Cell to)
        {
            await hubConnection.SendAsync("SendTryMove", from, to);
        }

        public bool IsConnected
        { get => hubConnection.State == HubConnectionState.Connected; }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
