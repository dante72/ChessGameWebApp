using Models;

namespace AuthService.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ILogger<RegistrationService> _logger;
        private readonly IUnitOfWork _uow;

        public RegistrationService(
            ILogger<RegistrationService> logger,
            IUnitOfWork uow
            )
        {
            _logger = logger;
            _uow = uow;

        }

        public async Task AddAccount(Account account)
        {
            await _uow.AccountRepository.Add(account);
            await _uow.SaveChangesAsync();
            _logger.LogDebug($"Create account {account.Login}");
        }

        public Task<IReadOnlyList<Account>> GetAccounts()
        {
            return _uow.AccountRepository.GetAll();
        }
        public Task GetAccountById(int id)
        {
            return _uow.AccountRepository.GetById(id);
        }

        public async Task BanAccount(Account account)
        {
            account.IsBanned = true;
            await _uow.AccountRepository.Update(account);
            await _uow.SaveChangesAsync();
        }

        public Task<Account> GetAccountByEmail(string email)
        {
            return _uow.AccountRepository.GetByEmail(email);
        }

        public Task<Account> GetAccountByLogin(string login)
        {
            return _uow.AccountRepository.GetByLogin(login);
        }
    }
}
