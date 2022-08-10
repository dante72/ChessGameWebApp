using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> FindByEmail(string email);
        Task<Account?> FindByLogin(string login);
    }
}
