using Microsoft.AspNetCore.SignalR;

namespace ChessGameWebApp.Server
{
    public class BroadcastHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
