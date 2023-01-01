using AuthWebAPI;
using Blazored.Modal;
using Blazored.Modal.Services;
using ChessGame;
using ChessGameWebApp.Client.Components;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class GameHubServiceImplV2 : GameHubServiceImpl
    {
        private IModalReference modalReferense;
        private readonly IModalService _modal;
        public GameHubServiceImplV2(ILogger<GameHubServiceImpl> logger,
                                    ChessBoard board,
                                    NavigationManager navigationManager,
                                    GameHttpClient httpClient,
                                    SiteUserInfo siteUserInfo,
                                    IModalService modal)
                            : base(logger,
                                   board,
                                   navigationManager,
                                   httpClient,
                                   siteUserInfo)
        {
            _modal = modal ?? throw new ArgumentNullException(nameof(modal));
        }

        public override void GetInviteAction()
        {
            modalReferense = _modal.Show<InviteComponent>("Invite");
        }

        public override void CloseInviteAction()
        { 
            modalReferense?.Close(); 
        }
    }
}
