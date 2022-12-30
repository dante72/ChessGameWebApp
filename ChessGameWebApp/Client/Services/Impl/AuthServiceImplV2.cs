﻿using AuthWebAPI;
using ChessGameWebApp.Client.Models;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class AuthServiceImplV2 : AuthServiceImpl
    {
        private readonly NavigationManager _navigationManager;
        public AuthServiceImplV2(ILogger<AuthServiceImpl> logger,
                                 IAuthWebApi authWebApi,
                                 SiteUserInfo siteUserInfo,
                                 IMyLocalStorageService localStorageService,
                                 TimeUpdater timeUpdater,
                                 NavigationManager navigationManager)
                        : base(logger,
                                authWebApi,
                                siteUserInfo,
                                localStorageService,
                                timeUpdater)
        {
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public override async Task LogOut()
        {
            await base.LogOut();
            _navigationManager.NavigateTo("/", true);
        }
    }
}
