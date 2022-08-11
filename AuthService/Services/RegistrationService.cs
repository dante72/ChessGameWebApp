using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Authentication;

namespace AuthService.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ILogger<RegistrationService> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly ITokenService _tokenService;

        public RegistrationService(
            ILogger<RegistrationService> logger,
            IUnitOfWork uow,
            IPasswordHasher<Account> passwordHasher,
            ITokenService tokenService
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task AddAccount(Account account)
        {
            try
            {
                account.Password = _passwordHasher.HashPassword(account, account.Password);
                await _uow.AccountRepository.Add(account);
                await _uow.SaveChangesAsync();
                _logger.LogInformation($"Account {account.Email} has been created!");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex.Message);
                throw new Exception("Login or e-mail already exists!");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new Exception("Unknown error!");
            }
        }

        public Task<IReadOnlyList<Account>> GetAccounts()
        {
            _logger.LogInformation(nameof(GetAccounts));
            return _uow.AccountRepository.GetAll();
        }
        public Task GetAccountById(int id)
        {
            _logger.LogInformation(nameof(GetAccountById));
            return _uow.AccountRepository.GetById(id);
        }

        public async Task BanAccount(Account account)
        {
            _logger.LogInformation($"BanAccount {account.Login}");
            account.IsBanned = true;
            await _uow.AccountRepository.Update(account);
            await _uow.SaveChangesAsync();
        }

        public Task<Account?> GetAccountByEmail(string email)
        {
            _logger.LogInformation(nameof(GetAccountByEmail));
            return _uow.AccountRepository.FindByEmail(email);
        }

        public Task<Account?> GetAccountByLogin(string login)
        {
            _logger.LogInformation(nameof(GetAccountByLogin));
            return _uow.AccountRepository.FindByLogin(login);
        }

        public async Task<JwtTokens?> GetTokens(string login, string password)
        {
            _logger.LogInformation(nameof(GetTokens));
            var account = await GetAccountByLogin(login) ?? await GetAccountByEmail(login);


            var isCorrect = account != null
                && _passwordHasher.VerifyHashedPassword(account, account.Password, password) != PasswordVerificationResult.Failed;

            if (!isCorrect)
                throw new AuthenticationException("Login or password are not correct!");
            
            var token = _tokenService.GenerateToken(account);
            
            return new JwtTokens() { AccessToken = token };
        }
    }
}
