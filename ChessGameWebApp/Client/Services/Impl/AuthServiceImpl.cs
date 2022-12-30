using AuthWebAPI;
using Blazored.LocalStorage;
using ChessGame;
using ChessGameWebApp.Client.Models;
using JwtToken;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Services.Impl
{
    public class AuthServiceImpl : IAuthService, IChessObserver, IDisposable
    {
        private readonly ILogger<AuthServiceImpl> _logger;
        private readonly IAuthWebApi _authWebApi;
        private readonly SiteUserInfo _siteUserInfo;
        private readonly ILocalStorageService _localStorageService;
        private readonly TimeUpdater _timeUpdater;
        private readonly NavigationManager _navigationManager;

        public AuthServiceImpl(ILogger<AuthServiceImpl> logger,
                           IAuthWebApi authWebApi,
                           SiteUserInfo siteUserInfo,
                           ILocalStorageService localStorageService,
                           TimeUpdater timeUpdater,
                           NavigationManager navigationManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authWebApi = authWebApi ?? throw new ArgumentNullException(nameof(authWebApi));
            _siteUserInfo = siteUserInfo ?? throw new ArgumentNullException(nameof(siteUserInfo));
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
            _timeUpdater = timeUpdater ?? throw new ArgumentNullException(nameof(timeUpdater));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));

            ((IChessObservable)timeUpdater).Subscribe(this);
        }

        public async Task<bool> Autorization(AccountRequestModel account)
        {
            var result = await _authWebApi.Autorization(account);

            if (result != null)
            {
                var access = TokenService.DecodeToken(result.AccessToken);
                _siteUserInfo.Update(access.Claims);

                await SetTokenToLocalStorage(result.RefreshToken);

                return true;
            }

            return false;
        }

        private async Task<string> GetTokenFromLocalStorage()
        {
            return await _localStorageService.GetItemAsync<string>("refresh" + _siteUserInfo.Id);
        }

        private async Task SetTokenToLocalStorage(string token)
        {
            await _localStorageService.SetItemAsync("refresh" + _siteUserInfo.Id, token);
        }

        private async Task RemoveTokenFromLocalStorage()
        {
            await _localStorageService.RemoveItemAsync("refresh" + _siteUserInfo.Id);
        }

        private async Task<string?> GetFirstTokenFromLocalStorage()
        {
            var keys = await _localStorageService.KeysAsync();
            var key = keys.FirstOrDefault(i => i.StartsWith("refresh"));

            return await _localStorageService.GetItemAsync<string>(key);
        }

        public async Task<bool> TokenAutorization()
        {
            var token = await GetFirstTokenFromLocalStorage();

            if (token == null)
                return false;

            var result = await _authWebApi.Autorization(token);

            if (result != null)
            {
                var access = TokenService.DecodeToken(result.AccessToken);

                _siteUserInfo.Update(access.Claims);

                await SetTokenToLocalStorage(result.RefreshToken);

                return true;
            }

            return false;
        }

        private object _lock = new object();
        public async void TryUpdateToken()
        {
            if (_siteUserInfo.AccessTokenExpire - DateTime.UtcNow > TimeSpan.FromSeconds(10))
                return;

            lock (_lock)
            {
                if (_siteUserInfo.AccessTokenExpire - DateTime.UtcNow > TimeSpan.FromSeconds(10))
                    return;
            }

            await GetTokens();
        }

        public async Task LogOut()
        {
            await RemoveTokenFromLocalStorage();
            await _authWebApi.SingOut();
            _siteUserInfo.Default();
            _navigationManager.NavigateTo("/", true);
        }

        private async Task GetTokens()
        {
            var refreshToken = await GetTokenFromLocalStorage();

            if (refreshToken == null)
            {
                await LogOut();
            }
            var result = await _authWebApi.Autorization(refreshToken);

            var access = TokenService.DecodeToken(result.AccessToken);

            _siteUserInfo.Update(access.Claims);

            if (result.RefreshToken != refreshToken)
                await SetTokenToLocalStorage(result.RefreshToken);
        }

        public Task UpdateAsync()
        {
            TryUpdateToken();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            ((IChessObservable)_timeUpdater).Remove(this);
        }
    }
}
