using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameService
    {
        IBoardViewModel GetBoard();
        IList<Cell> GetPossibleMoves(Cell cell);
        IList<Cell> GetPossibleMoves(int row, int column);
    }
}
