using AuthWebAPI;
using Castle.Windsor;
using ChessGameWebApp.Client.Models;
using ChessGameWebApp.Client.Services.Impl;

namespace ChessGameWebApp.Client.Services
{
    public class ChessGameClient
    {
        var container = new WindsorContainer();
        static IAuthWebApi authWebApi;
        static SiteUserInfo siteUserInfo;
        static IMyLocalStorageService localStorageService;
        static TimeUpdater timeUpdater;
        static ILogger<AuthServiceImpl> loggerAuthServiceImpl;

        public static IAuthService authService =
            new AuthServiceImpl(loggerAuthServiceImpl,
                                authWebApi,
                                siteUserInfo,
                                localStorageService,
                                timeUpdater);

        public IChatHubService chatHubService = new 
    }
}
