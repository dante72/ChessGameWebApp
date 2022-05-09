using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameService
    {
        ChessBoard GetBoard();
        ChessBoard Click(int row, int column);
    }
}
