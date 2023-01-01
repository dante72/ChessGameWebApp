using AuthWebAPI;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChessGame;
using ChessGameWebApp.Client.Models;
using ChessGameWebApp.Client.Services.Impl;
using ChessGameWebApp.Shared;

namespace ChessGameWebApp.Client.Services
{
    public class ChessGameClient
    {
        private readonly WindsorContainer container = new WindsorContainer();

        public readonly GameHttpClient gameHttpClient;
        public readonly AuthHttpClient authHttpClient;
        public readonly IAuthWebApi authWebApi;
        public readonly ChessBoard chessBoard;
        public readonly IClientGameService clientGameService;
        public readonly IGameHubService gameHubService;
        public readonly IChatHubService chatHubService;
        public readonly SiteUserInfo siteUserInfo;
        public readonly TimeUpdater timeUpdater;
        public readonly IMyLocalStorageService myLocalStorage;
        public readonly IAuthService authService;
        public readonly List<ChatMessage> messages;

        public ChessGameClient()
        {
            container.Register(Component.For<GameHttpClient>());
            gameHttpClient = container.Resolve<GameHttpClient>();

            container.Register(Component.For<AuthHttpClient>());
            authHttpClient = container.Resolve<AuthHttpClient>();

            container.Register(Component.For<IAuthWebApi>()
                .ImplementedBy<AuthWebApi>());
            authWebApi = container.Resolve<IAuthWebApi>();

            container.Register(Component.For<ChessBoard>());
            chessBoard = container.Resolve<ChessBoard>();

            container.Register(Component.For<IGameHubService>()
                .ImplementedBy<GameHubServiceImplV2>());
            gameHubService = container.Resolve<IGameHubService>();

            container.Register(Component.For<IChatHubService>()
                .ImplementedBy<ChatHubServiceImpl>());
            chatHubService = container.Resolve<IChatHubService>();

            container.Register(Component.For<SiteUserInfo>());
            siteUserInfo = container.Resolve<SiteUserInfo>();

            container.Register(Component.For<TimeUpdater>());
            timeUpdater = container.Resolve<TimeUpdater>();

            container.Register(Component.For<IMyLocalStorageService>()
                .ImplementedBy<MyLocalStorageServiceImpl>());
            myLocalStorage = container.Resolve<IMyLocalStorageService>();

            container.Register(Component.For<IAuthService>()
                .ImplementedBy<AuthServiceImplV2>());
            authService = container.Resolve<IAuthService>();

            container.Register(Component.For<List<ChatMessage>>());
            messages = container.Resolve<List<ChatMessage>>();
        }
    }
}
