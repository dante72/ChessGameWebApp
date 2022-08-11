using DbContextDao;
using Repository;

namespace Repository
{
    public class UnitOfWorkEF : IUnitOfWork, IAsyncDisposable
    {
        public IAccountRepository AccountRepository { get; }

        private readonly AuthContext _dbContext;

        public UnitOfWorkEF(
            AuthContext dbContext,
            IAccountRepository accountRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
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
