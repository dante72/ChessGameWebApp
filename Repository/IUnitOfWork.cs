using Repository;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        Task SaveChangesAsync();
    }
}
