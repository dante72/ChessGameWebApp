using Repositories;

namespace Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        Task SaveChangesAsync();
    }
}
