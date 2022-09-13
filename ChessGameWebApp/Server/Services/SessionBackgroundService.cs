using ChessGame;
using ChessGameWebApp.Server.Models;
using Player = ChessGameWebApp.Server.Models.Player;

namespace ChessGameWebApp.Server.Services
{
    public class SessionBackgroundService : BackgroundService
    {
        private readonly List<GameSession> _sessions;
        private readonly List<Player> _players;
        private readonly IGameHubService _gameHub;
        private readonly ILogger<SessionBackgroundService> _logger;
        private Task _task;
        public SessionBackgroundService(List<GameSession> sessions, List<Player> players, IGameHubService gameHub, ILogger<SessionBackgroundService> logger)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _gameHub = gameHub ?? throw new ArgumentNullException(nameof(gameHub));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_task == null)
            {
                _task = CreateTask();
                _task.Start();
            }

            return Task.CompletedTask;
        }

        private Task CreateTask()
        {
            return new Task(async () =>
            {
                while (true)
                {
                    await TryCreateSession();

                    _logger.LogInformation("service is working...");
                    await Task.Delay(10_000);
                }
            });
        }

        private async Task TryCreateSession()
        {
            var session = GameSessionService.Create(_players);
            if (session != null)
            {

                lock (_sessions)
                    _sessions.Add(session);

                await _gameHub.StartGame(session.Players);
            }
        }
    }
}
