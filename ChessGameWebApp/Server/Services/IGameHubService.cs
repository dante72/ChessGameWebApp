using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameHubService
    {
        Task StartGame(IList<Player> players);
    }
}