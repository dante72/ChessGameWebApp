using ChessGame;
using ChessGameWebApp.Shared;
using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace AuthWebAPI
{
    public class AuthWebApi
    {
        private readonly HttpClient _auth;
        private readonly HttpClient _gameServer;
        public AuthWebApi(HttpClient auth, HttpClient gameServer)
        {
            _auth = auth ?? throw new NullReferenceException(nameof(auth));
            _gameServer = gameServer ?? throw new NullReferenceException(nameof(gameServer));
        }

        public async Task Registration(AccountRequestModel account)
        {
            try
            {
                var res = await _auth.PostAsJsonAsync($"Auth", account);
            }
            catch (Exception ex)
            {
                var str = ex.Message;
                int i = 10;
            }
        }

        public async Task<JwtTokens?> Autorization(AccountRequestModel account)
        {
            try
            {
                var result = await _auth.GetFromJsonAsync<JwtTokens>($"Auth?login={account.Login}&password={account.Password}");
                _auth.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", result?.AccessToken);
                _gameServer.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", result?.AccessToken);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<WeatherForecast[]> Weather()
        {
            var result = await _auth.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");

            return result;
        }
    }
}