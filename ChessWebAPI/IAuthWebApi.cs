using ChessGameWebApp.Shared;
using JwtToken;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWebAPI
{
    public interface IAuthWebApi
    {
        Task Registration(AccountRequestModel account);
        Task<JwtTokens?> Autorization(AccountRequestModel account);
        Task<JwtTokens?> Autorization(string refreshToken);
        Task<WeatherForecast[]> Weather();
        Task<UserInfo?> GetUserInfo();
        Task SingOut();
        Task<bool> AddOrRemovePlayer();
        Task<int> PlayerCount();
        Task<bool> SessionExists();
    }
}
