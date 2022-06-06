using ChessGame;

namespace ChessGameWebApp.Client.Services
{
    public interface IClientGameService
    {
        Task GetBoard();
        Task<ChessBoard> GetBoard2();
        Task<ChessCellDto> GetCell();
        Task<Figure> GetFigure();
        void BoardUpdateFromServer();
    }
}
