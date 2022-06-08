using ChessGame;
using ChessWebAPI;

namespace ChessGameWebApp.Client.Services
{
    public class ClientGameService : IClientGameService
    {
        private readonly ILogger<ClientGameService> _logger;
        private readonly ChessBoard _board;
        IGameHubService _gameHubService;

        public ChessCell[] CurrentMove { get; set; } = new ChessCell[0];
        public ClientGameService(ILogger<ClientGameService> logger, ChessBoard board, IGameHubService gameHubService)
        {
            _logger = logger;
            _board = board;
            _gameHubService = gameHubService;
            _board.SetCheckMethod(TryMove);
        }

        public async Task<bool> TryMove(Cell from, Cell to)
        {
            await _gameHubService.SendTryMove(from, to);
            return true;
        }
    }
}
