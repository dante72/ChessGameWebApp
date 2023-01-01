using AuthWebAPI;
using ChessGame;
using ChessGameWebApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class ChatHubServiceImpl : IChatHubService
    {
        private readonly ILogger<ChatHubServiceImpl> _logger;
        private readonly HubConnection _hubConnection;
        private readonly GameHttpClient _httpClient;

        public delegate Task Updater();
        private Updater _updater;

        public ChatHubServiceImpl(ILogger<ChatHubServiceImpl> logger,
                              NavigationManager navigationManager,
                              GameHttpClient httpClient,
                              List<ChatMessage> messages)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chathub"), options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_httpClient.DefaultRequestHeaders.Authorization?.Parameter);
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<ChatMessage>("ReceiveMessage", (param) =>
            {
                var encodedMsg = $"{param.Time:hh\\:mm\\:ss} {param.Username}: {param.Message}";
                messages.Insert(0, param);

                if (_updater != null)
                    _updater();
            });
        }

        public void SetUpdater(Updater updater) => _updater = updater;

        public async Task Start()
        {
            if (!IsConnected)
                await _hubConnection.StartAsync();
        }

        public async Task Send(string message)
        {
            if (!IsConnected)
                await _hubConnection.StartAsync();

            await _hubConnection.SendAsync("SendMessage", message);
        }

        public bool IsConnected =>
            _hubConnection.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.StopAsync();
                //await _hubConnection.DisposeAsync();
            }
        }
    }
}
