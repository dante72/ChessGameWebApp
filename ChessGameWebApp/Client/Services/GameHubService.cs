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
        private readonly HubConnection hubConnection;
        private readonly SiteUserInfo _siteUserInfo;

        public ChessCell[] CurrentMove { get; set; } = new ChessCell[0];
        public GameHubService(ILogger<GameHubService> logger, ChessBoard board, NavigationManager navigationManager, HttpClient httpClient, SiteUserInfo siteUserInfo)
        {
            _logger = logger;
            _board = board;
            _navigationManager = navigationManager;
            _siteUserInfo = siteUserInfo;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("/gamehub"), options =>
                 {
                     options.AccessTokenProvider = () => Task.FromResult(httpClient.DefaultRequestHeaders.Authorization.Parameter);
                 })
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<ChessBoardDto, FigureColors, ChessTimerDto>("ReceiveBoard", (board, playerColor, timer) =>
            {
                _board.SetCurrentPlayer(playerColor);
                _board.Update(board);
                _board.SetTimer(timer);
            });

            hubConnection.On<ChessCellDto, ChessCellDto, ChessTimerDto>("ReceiveTryMove", (from, to, timer) =>
            {
                var fromCell = _board.GetCell(from.Row, from.Column);
                var toCell = _board.GetCell(to.Row, to.Column);

                _board.TryMove(fromCell, toCell);

                if (timer != null)
                    _board.SetTimer(timer);
            });

            hubConnection.On<bool>("StartGame", (start) => 
            {
                if (start)
                    navigationManager.NavigateTo("/Game/start");
            });
            
            hubConnection.On<bool>("ReceiveMoveBack", (ok) =>
            {
                if (ok)
                    _board.TryMoveBack();
            });

            hubConnection.On<int, bool>("ChangeStatus", (id, status) =>
            {
                lock(_siteUserInfo)
                {
                    if (_siteUserInfo.Id == id)
                        _siteUserInfo.Status = status;
                    else
                        _siteUserInfo.RivalStatus = status;
                }
            });

        }
        public async Task MoveBack()
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("MoveBack");
        }

        public async Task GetBoard()
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("SendBoard");
        }

        public async Task SendTryMove(Cell from, Cell to)
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("SendTryMove", from, to);
        }

        public async Task StartGame()
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("StartGame", false);
        }

        public async Task GameOver()
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("GameOver");
        }

        public async Task AddOrRemovePlayer(int rivalId = 0)
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("AddOrRemovePlayer", rivalId);
        }

        public bool IsConnected
        { get => hubConnection.State == HubConnectionState.Connected; }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
            }
        }
    }
}
