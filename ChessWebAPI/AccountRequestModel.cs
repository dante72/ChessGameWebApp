using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebAPI
{
    public class AccountRequestModel
    {
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public AccountRequestModel() { }

        public AccountRequestModel(string login, string password, string email, string username)
        {
            Login = login;
            Password = password;
            Email = email;
            Username = username;
        }
    }
}
