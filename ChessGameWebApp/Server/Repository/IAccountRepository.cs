using Models;

namespace ChessGameWebApp.Server.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByEmail(string email);
        Task<Account?> GetByLogin(string login);
    }
}
