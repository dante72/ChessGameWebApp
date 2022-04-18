using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    internal class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
        }
        public Board GetBoard()
        {
            return new Board();
        }
    }
}
