﻿using Models;

namespace AuthService.Services
{
    public interface IRegistrationService
    {
        Task AddAccount(Account account);
        Task<Account> GetAccountByEmail(string email);
        Task BanAccount(Account account);
        Task<IReadOnlyList<Account>> GetAccounts();
    }
}
