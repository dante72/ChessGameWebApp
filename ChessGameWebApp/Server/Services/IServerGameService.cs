using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IServerGameService
    {
        ChessBoard GetBoard();
        bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn);
    }
}
