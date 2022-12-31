using AuthWebAPI;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Models;
using ChessGameWebApp.Client.Services;
using ChessGameWebApp.Client.Services.Impl;
using ChessGameWebApp.Shared;
using Microsoft.Extensions.Logging;

namespace ChessGameWebApp.Client.Services
{
    public class ChessGameClient
    {
        /*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
         builder.Services.AddScoped(sp => new AuthHttpClient { BaseAddress = new Uri("https://localhost:7256/") });
builder.Services.AddScoped<IAuthWebApi, AuthWebApi>();
builder.Services.AddScoped(b => new ChessBoard());
builder.Services.AddScoped<IClientGameService, ClientGameServiceImpl>();
builder.Services.AddScoped<IGameHubService, GameHubServiceImplV2>();
builder.Services.AddScoped<IChatHubService, ChatHubServiceImpl>();
builder.Services.AddScoped(user => new SiteUserInfo());
builder.Services.AddScoped(updater => new TimeUpdater());
builder.Services.AddScoped<IMyLocalStorageService, MyLocalStorageServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImplV2>();
builder.Services.AddScoped(chat => new List<ChatMessage>());*/

public readonly WindsorContainer container = new WindsorContainer();

        public ChessGameClient()
        {
            container.Register(Component.For<ChessBoard>());
        }
    }
}
