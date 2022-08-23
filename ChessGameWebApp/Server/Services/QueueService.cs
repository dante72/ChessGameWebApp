using ChessGameWebApp.Shared;

namespace ChessGameWebApp.Server.Services
{
    public class QueueService : IQueueService
    {
        private readonly List<Player> _players = new List<Player>();

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
