using ChessGame;

namespace ChessGameWebApp.Client.Services
{
    public interface IGameHubService
    {
        Task GetBoard();
        bool IsConnected { get; }
        Task SendTryMove(Cell from, Cell to);
        Task StartGame();
        Task MoveBack();
        Task GameOver();
        Task AddOrRemovePlayer(int rivalId = 0);
        Task SendInvite(int rivalId);
    }
}
