using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameService
    {
        IBoardViewModel GetBoard();
        List<Cell> GetPossibleMoves(int row, int column);
    }
}
