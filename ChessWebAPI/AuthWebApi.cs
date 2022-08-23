using ChessGameWebApp.Shared;
using JwtToken;
using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AuthWebAPI
{
    public class AuthWebApi : IAuthWebApi
    {
        public readonly AuthHttpClient _auth;
        public readonly HttpClient _gameServer;
        public AuthWebApi(AuthHttpClient auth, HttpClient gameServer)
        {
            _auth = auth ?? throw new NullReferenceException(nameof(auth));
            _gameServer = gameServer ?? throw new NullReferenceException(nameof(gameServer));
        }

        public async Task Registration(AccountRequestModel account)
        {
            try
            {
                var res = await _auth.PostAsJsonAsync($"Auth/Registaration", account);
            }
            catch
            {
                throw;
            }
        }

        public async Task<JwtTokens?> Autorization(AccountRequestModel account)
        {
            try
            {
                var result = await _auth.GetFromJsonAsync<JwtTokens>($"Auth/Authentication?login={account.Login}&password={account.Password}");
                _auth.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", result?.AccessToken);
                _gameServer.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", result?.AccessToken);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public Task SingOut()
        {
            _auth.DefaultRequestHeaders.Authorization = null;
            _gameServer.DefaultRequestHeaders.Authorization = null;


            return Task.CompletedTask;
        }

        public async Task<WeatherForecast[]> Weather()
        {
            var result = await _gameServer.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");

            return result;
        }

        public async Task<UserInfo?> GetUserInfo()
        {
            return await _gameServer.GetFromJsonAsync<UserInfo?>("ChessGame/GetUser");

        }
    }
}