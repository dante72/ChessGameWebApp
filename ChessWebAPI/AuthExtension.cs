using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWebAPI
{
    public static class AuthExtension
    {
        public static Account Map(this AccountRequestModel account)
        {
            var acc = new Account();

            acc.Username = account.Username;
            acc.Login = account.Login;
            acc.Email = account.Email;
            acc.Password = account.Password;

            return acc;
        }
    }
}
