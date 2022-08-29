using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly List<Player> _players;

        public PlayerService(List<Player> players)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        public Task Add(Player player)
        {
            lock (_players)
            {
                _players.Add(player);
            }

            return Task.CompletedTask;
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
