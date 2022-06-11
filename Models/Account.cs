using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account : IEntity
    {
        public Account(int id, string login, string email, string hashPassword)
        {
            Id = id;
            Login = login;
            Email = email;
            HashPassword = hashPassword;
            IsBanned = false;
        }

        public Account()
        {
            Id = 0;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string HashPassword { get; set; }

        public bool IsBanned { get; set; }

    }
}
