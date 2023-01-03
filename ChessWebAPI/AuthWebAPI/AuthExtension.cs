using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameClient.AuthWebAPI
{
    public static class AuthExtension
    {
        public static Account Map(this AccountRequestModel account)
        {
            var acc = new Account
            {
                Username = account.Username,
                Login = account.Login,
                Email = account.Email,
                Password = account.Password
            };

            return acc;
        }
    }
}
