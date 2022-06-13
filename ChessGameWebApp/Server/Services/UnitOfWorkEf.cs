using ChessGameWebApp.Server.Repository;

namespace ChessGameWebApp.Server.Services;

public class UnitOfWorkEf : IUnitOfWork, IAsyncDisposable
{
    public IAccountRepository AccountRepository { get; }
    
    private readonly AppDbContext _dbContext;

    public UnitOfWorkEf(
        AppDbContext dbContext,
        IAccountRepository accountRepository) 
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}
