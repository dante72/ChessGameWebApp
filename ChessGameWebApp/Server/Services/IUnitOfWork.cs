using ChessGameWebApp.Server.Repository;

namespace ChessGameWebApp.Server.Services;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}