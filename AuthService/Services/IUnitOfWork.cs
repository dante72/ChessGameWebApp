using Repository;

namespace AuthService.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        Task SaveChangesAsync();
    }
}
