using ChessGame;
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

        public Task Registration(AccountRequestModel account)
        {
            return _httpClient.PostAsJsonAsync($"/Registration/Registration", account);
        }
    }
}