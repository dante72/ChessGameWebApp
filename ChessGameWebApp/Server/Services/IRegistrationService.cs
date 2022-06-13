using Models;
using Microsoft.AspNetCore.Mvc;
using ChessWebAPI;

namespace ChessGameWebApp.Server.Services;

public interface IRegistrationService
{
    Task AddAccount(Account account);
    Task<Account> GetAccountByEmail(string email);
    Task BanAccount(Account account);
}