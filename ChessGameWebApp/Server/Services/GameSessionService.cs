using ChessGame;
using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly List<GameSession> _sessions;
        private readonly List<Player> _players;
        public GameSessionService(List<GameSession> sessions, List<Player> players)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        public Task Create(int[] playerIds)
        {
            IEnumerable<Player> players;
            lock (_players)
            {
                players = _players.Where(p => playerIds.Contains(p.Id));
                foreach (var player in players)
                    _players.Remove(player);
            }
            var sesion = new GameSession()
            {
                Players = players.ToList(),
                Board = new ChessBoard()
            };

            lock (_sessions)
                _sessions.Add(sesion);

            return Task.CompletedTask;
        }
    }
}
