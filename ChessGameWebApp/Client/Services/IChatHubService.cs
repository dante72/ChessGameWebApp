using static ChessGameWebApp.Client.Services.ChatHubService;

namespace ChessGameWebApp.Client.Services
{
    public interface IChatHubService
    {
        bool IsConnected { get; }
        Task Send(string message);
        ValueTask DisposeAsync();
        Task Start();
        void SetUpdater(Updater updater);
    }
}
