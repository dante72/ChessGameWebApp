using ChessGame;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class ClientGameServiceImpl : IClientGameService
    {
        private readonly ILogger<ClientGameServiceImpl> _logger;
        private readonly ChessBoard _board;
        IGameHubService _gameHubService;

        public ClientGameServiceImpl(ILogger<ClientGameServiceImpl> logger, ChessBoard board, IGameHubService gameHubService)
        {
            _logger = logger;
            _board = board;
            _gameHubService = gameHubService;
            _board.SetCheckMethod(TryMove);
        }

        public async Task<bool> TryMove(Cell from, Cell to)
        {
            await _gameHubService.SendTryMove(from.ToDto(), to.ToDto());
            return false;
        }
    }
}
