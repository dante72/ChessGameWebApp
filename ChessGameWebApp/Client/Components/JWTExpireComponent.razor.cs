using AuthWebAPI;
using Blazored.LocalStorage;
using ChessGame;
using ChessGameWebApp.Client.Models;
using JwtToken;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class JWTExpireComponentModel : ComponentBase, IChessObserver, IDisposable
    {
        [Inject]
        SiteUserInfo User { get; }

        [Inject]
        TimeUpdater Updater { get; }

        [Inject]
        AuthWebApi AuthWebAPI { get; }

        [Inject]
        ILocalStorageService localStore { get; }

        [Parameter]
        public DateTime Time { get; set; }

        protected override void OnInitialized()
        {
            ((IChessObservable)Updater).Subscribe(this);
        }

        public async void Update()
        {
            if (User.AccessTokenExpire <= DateTime.UtcNow)
            {
                var refreshToken = await localStore.GetItemAsync<string>("refresh");
                var result = await AuthWebAPI.Autorization(refreshToken);

                var access = TokenService.DecodeToken(result.AccessToken);
                User.Update(access.Claims);

                if (result.RefreshToken != refreshToken)
                    await localStore.SetItemAsync("refresh", result.RefreshToken);
            }

            StateHasChanged();
        }

        public void Dispose()
        {
            ((IChessObservable)Updater).Remove(this);
        }
    }
}
