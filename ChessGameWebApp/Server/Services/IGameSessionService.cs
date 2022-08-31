using ChessGame;
using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public interface IGameSessionService
    {
        Task<GameSession> GetSession(int accountId);
    }
}
