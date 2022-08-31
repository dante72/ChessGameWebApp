using ChessGame;
using ChessGameWebApp.Server.Models;
using System.Text.Json;

namespace ChessGameWebApp.Server.Services
{
    internal class GameSessionService : IGameSessionService
    {
        private readonly ILogger<GameSessionService> _logger;
        private readonly List<GameSession> _sessions;
        public GameSessionService(ILogger<GameSessionService> logger, List<GameSession> sessions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }
        public Task<GameSession> GetSession(int accountId)
        {
            GameSession session;
            lock(_sessions)
                session = _sessions.First(s => s.Players.Any(p => p.Id == accountId));
            _logger.LogInformation($"Get session by {accountId}");
            return Task.FromResult(session);
        }
    }
}
