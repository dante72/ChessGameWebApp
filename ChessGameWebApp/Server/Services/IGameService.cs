using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameService
    {
        ChessBoard GetBoard();
        List<Cell> GetPossibleMoves(int row, int column);
        void Move(int fromRow, int fromColumn, int toRow, int toColumn);
        ChessBoard Click(int row, int column);
    }
}
