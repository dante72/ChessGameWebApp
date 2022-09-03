using ChessGame;
using ChessGameWebApp.Server.Models;

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
            List<Player> players = new List<Player>();
            int count = 0;
            lock (_players)
                count = _players.Count;

                if (count > 1)
                {
                    lock (_players)
                    {
                        if (_players.Count > 1)
                        {
                            players.Add(_players[0]);
                            players.Add(_players[1]);

                            foreach (var player in players)
                                _players.Remove(player);
                        }
                        var sesion = new GameSession()
                        {
                            Players = players.ToList(),
                            Board = new Board(true)
                        };

                        lock (_sessions)
                            _sessions.Add(sesion);
                        
                    }
                    await _gameHub.StartGame(players);
                }
        }
    }
}
