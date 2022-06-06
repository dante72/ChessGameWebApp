using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IServerGameService
    {
        ChessBoard GetBoard();
        ChessBoard Click(int row, int column);
    }
}
