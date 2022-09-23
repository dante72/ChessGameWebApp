using AuthWebAPI;
using Blazored.LocalStorage;
using ChessGame;
using ChessGameWebApp.Client.Models;
using JwtToken;

namespace ChessGameWebApp.Client.Services
{
    public class AuthService : IAuthService, IChessObserver, IDisposable
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthWebApi _authWebApi;
        private readonly SiteUserInfo _siteUserInfo;
        private readonly ILocalStorageService _localStorageService;
        private readonly TimeUpdater _timeUpdater;
        public AuthService(ILogger<AuthService> logger,
                           IAuthWebApi authWebApi,
                           SiteUserInfo siteUserInfo,
                           ILocalStorageService localStorageService,
                           TimeUpdater timeUpdater)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authWebApi = authWebApi ?? throw new ArgumentNullException(nameof(authWebApi));
            _siteUserInfo = siteUserInfo ?? throw new ArgumentNullException(nameof(siteUserInfo));
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
            _timeUpdater = timeUpdater ?? throw new ArgumentNullException(nameof(timeUpdater));

            ((IChessObservable)timeUpdater).Subscribe(this);
        }

        public async Task<bool> Autorization(AccountRequestModel account)
        {
            var result = await _authWebApi.Autorization(account);

            if (result != null)
            {
                var access = TokenService.DecodeToken(result.AccessToken);
                _siteUserInfo.Update(access.Claims);

                await _localStorageService.SetItemAsync("refresh", result.RefreshToken);

                return true;
            }

            return false;
        }

        private object _lock = new object();
        public async void TryUpdateToken()
        {
            if (_siteUserInfo.AccessTokenExpire - DateTime.UtcNow > TimeSpan.FromSeconds(2))
                return;

            lock(_lock)
            {
                if (_siteUserInfo.AccessTokenExpire - DateTime.UtcNow > TimeSpan.FromSeconds(2))
                    return;
            }
            
            await GetTokens();               
        }

        private async Task GetTokens()
        {
            var refreshToken = await _localStorageService.GetItemAsync<string>("refresh");
            var result = await _authWebApi.Autorization(refreshToken);

            var access = TokenService.DecodeToken(result.AccessToken);

            _siteUserInfo.Update(access.Claims);

            if (result.RefreshToken != refreshToken)
                await _localStorageService.SetItemAsync("refresh", result.RefreshToken);
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
