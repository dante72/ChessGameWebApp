using ChessGameWebApp.Shared;

namespace ChessGameWebApp.Server.Services
{
    public interface IQueueService
    {
        Task Add(Player player);
        Task Remove(Player player);
        Task<int> Count();
    }
}
