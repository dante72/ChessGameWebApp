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

        private Account Hash(AccountRequestModel account)
        {
            var acc = new Account();

            acc.Username = account.Username;
            acc.Login = account.Login;
            acc.Email = account.Email;
            string hashedPassword = _passwordHasher.HashPassword(acc, account.Password);

            acc.HashPassword = hashedPassword;

            return acc;
        }
    }
}
