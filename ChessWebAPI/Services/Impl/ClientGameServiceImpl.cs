using ChessGame;

namespace ChessGameClient.Services.Impl
{
    public class ClientGameServiceImpl : IClientGameService
    {
        private readonly ChessBoard _board;
        IGameHubService _gameHubService;

        public ClientGameServiceImpl(ChessBoard board, IGameHubService gameHubService)
        {
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
