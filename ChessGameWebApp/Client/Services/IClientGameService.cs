using ChessGame;

namespace ChessGameWebApp.Client.Services
{
    public interface IClientGameService
    {
        Task<bool> TryMove(Cell from, Cell to);
    }
}
