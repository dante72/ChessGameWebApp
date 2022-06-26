using ChessGame;
using ChessGameWebApp.Shared;
using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ChessWebAPI
{
    public class ServerWebApi
    {
        private readonly HttpClient _httpClient;
        public ServerWebApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new NullReferenceException(nameof(httpClient));
        }

        public async Task Registration(AccountRequestModel account)
        {
            await _httpClient.PutAsJsonAsync("Authentication/Registration", account);
        }

        public Task<Account[]?> GetAccounts()
        {
            return _httpClient.GetFromJsonAsync<Account[]>("Authentication/Accounts");
        }

        public async Task<AccountResponseModel?> Autorization(AccountRequestModel account)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Authentication/Autor", account);
                response.EnsureSuccessStatusCode(); //кидаем исключение при плохом статусе
                var result = await response.Content.ReadFromJsonAsync<AccountResponseModel>();
                _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", result?.Token);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}