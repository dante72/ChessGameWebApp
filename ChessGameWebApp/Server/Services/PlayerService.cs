using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly List<Player> _players;
        private readonly IGameHubService _gameHub;

        public PlayerService(IGameHubService gameHub, List<Player> players)
        {
            _gameHub = gameHub ?? throw new ArgumentNullException(nameof(gameHub));
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        public async Task Add(Player player)
        {
            lock (_players)
            {
                _players.Add(player);
            }
            await _gameHub.StartGame();
        }

        public Task Remove(Player player)
        {
            lock (_players)
            {
                if (_players.Contains(player))
                    _players.Remove(player);
            }
            return Task.CompletedTask;
        }

        public Task<int> Count()
        {
            lock(_players)
            {
                return Task.FromResult(_players.Count);
            }
        }
    }
}
