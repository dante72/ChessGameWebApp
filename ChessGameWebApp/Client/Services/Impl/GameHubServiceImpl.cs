﻿using Blazored.Modal;
using Blazored.Modal.Services;
using ChessGame;
using ChessGameWebApp.Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class GameHubServiceImpl : IGameHubService, IAsyncDisposable
    {
        private readonly ILogger<GameHubServiceImpl> _logger;
        private readonly ChessBoard _board;
        private readonly NavigationManager _navigationManager;
        private readonly HubConnection hubConnection;
        private readonly SiteUserInfo _siteUserInfo;
        private readonly IModalService _modal;
        private readonly HttpClient _httpClient;

        private IModalReference modalReferense;

       // public ChessCell[] CurrentMove { get; set; } = new ChessCell[0];
        public GameHubServiceImpl(ILogger<GameHubServiceImpl> logger,
                              ChessBoard board,
                              NavigationManager navigationManager,
                              HttpClient httpClient,
                              SiteUserInfo siteUserInfo,
                              IModalService modal)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
            _siteUserInfo = siteUserInfo ?? throw new ArgumentNullException(nameof(siteUserInfo));
            _modal = modal ?? throw new ArgumentNullException(nameof(modal));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("/gamehub"), options =>
                 {
                     options.AccessTokenProvider = () => Task.FromResult(_httpClient.DefaultRequestHeaders.Authorization?.Parameter);
                 })
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<ChessBoardDto, FigureColor, ChessTimerDto>("ReceiveBoard", (board, playerColor, timer) =>
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
                lock (_siteUserInfo)
                {
                    if (_siteUserInfo.Id == id)
                        _siteUserInfo.Status = status;
                    else
                    {
                        _siteUserInfo.RivalStatus = status;
                    }
                }
            });

            hubConnection.On<int, string>("GetInvite", (id, rivalName) =>
            {
                _siteUserInfo.RivalId = id;
                _siteUserInfo.RivalName = rivalName;
                modalReferense = _modal.Show<InviteComponent>("Invite");
            });

            hubConnection.On("CloseInvite", () =>
            {
                modalReferense?.Close();
            });

            InitConnection();
        }

        public async void InitConnection()
        {
            await StartGame();
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

        public async Task SendTryMove(ChessCellDto from, ChessCellDto to)
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

        public async Task SendInvite(int rivalId, string rivalName)
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("SendInvite", rivalId, rivalName);
        }

        public async Task CloseInvite(int rivalId)
        {
            if (!IsConnected)
                await hubConnection.StartAsync();
            await hubConnection.SendAsync("CloseInvite", rivalId);
        }

        public bool IsConnected
        { get => hubConnection.State == HubConnectionState.Connected; }


        public Task Help()
        {
            _board.GetHelp();

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.StopAsync();
                //await hubConnection.DisposeAsync();
            }
        }
    }
}
