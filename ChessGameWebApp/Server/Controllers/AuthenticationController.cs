using ChessGameWebApp.Server.Services;
using ChessGameWebApp.Shared;
using ChessWebAPI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public AuthenticationController(ILogger<AuthenticationController> logger, IRegistrationService registrationService, IPasswordHasher<Account> passwordHasher)
        {
            _logger = logger;
            _registrationService = registrationService;
            _passwordHasher = passwordHasher;
        }

        [HttpPut("Registration")]
        public async Task Registration([FromBody] AccountRequestModel account)
        {
            _logger.Log(LogLevel.Information, $"Registration {account.Login}");
            await _registrationService.AddAccount(Hash(account));
        }

        [HttpGet("Accounts")]
        public Task<IReadOnlyList<Account>> Accounts()
        {
            return _registrationService.GetAccounts();
        }

        [HttpPost("Autor")]
        public async Task<ActionResult<AccountResponseModel>> Autorization([FromBody] AccountRequestModel account)
        {
            _logger.Log(LogLevel.Information, $"Autorization {account.Login}");
            return await _registrationService.Autorization(account);

        }

        private Account Hash(AccountRequestModel account)
        {
            var acc = new Account();

            acc.Id = 0;
            acc.Username = account.Username;
            acc.Login = account.Login;
            acc.Email = account.Email;
            string hashedPassword = _passwordHasher.HashPassword(acc, account.Password);

            acc.HashPassword = hashedPassword;

            return acc;
        }
    }
}
