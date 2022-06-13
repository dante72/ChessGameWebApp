using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChessWebAPI;

namespace ChessGameWebApp.Server.Services;

public class RegistrationService : IRegistrationService
{
    private readonly ILogger<RegistrationService> _logger;
    private readonly IPasswordHasher<Account> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _uow;

    public RegistrationService(
        ILogger<RegistrationService> logger,
        IPasswordHasher<Account> passwordHasher,
        ITokenService tokenService,
        IUnitOfWork uow
        )
    {
        _logger = logger;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _uow = uow;

    }
    
    public async Task AddAccount(Account account)
    { 
            await _uow.AccountRepository.Add(account);
            await _uow.SaveChangesAsync();

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