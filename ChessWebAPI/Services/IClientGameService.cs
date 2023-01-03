using ChessGame;

namespace ChessGameClient.Services
{
    public interface IClientGameService
    {
        Task<bool> TryMove(Cell from, Cell to);
    }
}
