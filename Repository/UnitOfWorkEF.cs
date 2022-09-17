using DbContextDao;
using Repositories;

namespace Repositories
{
    public class UnitOfWorkEF : IUnitOfWork, IAsyncDisposable
    {
        public IAccountRepository AccountRepository { get; }
        public IRoleRepository RoleRepository { get; }

        private readonly AuthContext _dbContext;

        public UnitOfWorkEF(
            AuthContext dbContext,
            IAccountRepository accountRepository,
            IRoleRepository roleRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public Task SaveChangesAsync()
        {
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Dispose() => _dbContext.Dispose();
        public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
    }
}
