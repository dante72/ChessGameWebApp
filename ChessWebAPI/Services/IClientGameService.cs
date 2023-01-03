using ChessGame;

namespace AuthWebAPI.Services
{
    public interface IClientGameService
    {
        Task<bool> TryMove(Cell from, Cell to);
    }
}
