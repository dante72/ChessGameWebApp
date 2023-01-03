using static AuthWebAPI.Services.Impl.ChatHubServiceImpl;

namespace AuthWebAPI.Services
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
