using AuthWebAPI;
using Blazored.LocalStorage;
using JwtToken;

namespace ChessGameWebApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthWebApi _authWebApi;
        private readonly SiteUserInfo _siteUserInfo;
        private readonly ILocalStorageService _localStorageService;
        public AuthService(ILogger<AuthService> logger,
                           IAuthWebApi authWebApi,
                           SiteUserInfo siteUserInfo,
                           ILocalStorageService localStorageService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authWebApi = authWebApi ?? throw new ArgumentNullException(nameof(authWebApi));
            _siteUserInfo = siteUserInfo ?? throw new ArgumentNullException(nameof(siteUserInfo));
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
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
    }
}
