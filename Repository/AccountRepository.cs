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

        public Task<Account?> GetByEmail(string email)
            => _entities.FirstOrDefaultAsync(it => it.Email == email);

        public Task<Account?> GetByLogin(string login)
            => _entities.FirstOrDefaultAsync(it => it.Login == login);
    }
}
