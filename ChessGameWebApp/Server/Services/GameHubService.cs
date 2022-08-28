using ChessGameWebApp.Server.SignalRHub;
using Microsoft.AspNetCore.SignalR;

namespace ChessGameWebApp.Server.Services
{
    public class GameHubService : IGameHubService
    {
        IHubContext<GameHub> _hubContext;
        public GameHubService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task StartGame()
        {
            await _hubContext.Clients.All.SendAsync("StartGame", true);
        }

    }
}
