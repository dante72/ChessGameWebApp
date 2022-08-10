using DbContextDao;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AccountRepository : EfRepository<Account>, IAccountRepository
    {
        public AccountRepository(AuthContext dbContext) : base(dbContext)
        {
        }

        public Task<Account?> FindByEmail(string email)
        {
            var result = _entities.FirstOrDefault(it => it.Email == email);
            return Task.FromResult(result);
        }

        public Task<Account?> FindByLogin(string login)
        {
            var result = _entities.FirstOrDefault(it => it.Login == login);
            return Task.FromResult(result);
        }
    }
}
