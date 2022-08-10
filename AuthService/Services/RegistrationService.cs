﻿using Microsoft.EntityFrameworkCore;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));

        }

        public async Task AddAccount(Account account)
        {
            try
            {
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

        public Task<Account?> GetAccountByEmail(string email)
        {
            return _uow.AccountRepository.FindByEmail(email);
        }

        public Task<Account?> GetAccountByLogin(string login)
        {
            return _uow.AccountRepository.FindByLogin(login);
        }
    }
}
