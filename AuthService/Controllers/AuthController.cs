using AuthService.Services;
using ChessWebAPI;
using DbContextDao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Net;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IPasswordHasher<Account> _passwordHasher;
        public AuthController(IRegistrationService registrationService, IPasswordHasher<Account> passwordHasher)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }
        [HttpPost(Name = "Registaration")]
        public async Task<IActionResult> Registration([FromBody]AccountRequestModel account)
        {
            try
            {
                await _registrationService.AddAccount(Map(account));
                return Ok("Registration is successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Account Map(AccountRequestModel account)
        {
            var acc = new Account();

            acc.Username = account.Username;
            acc.Login = account.Login;
            acc.Email = account.Email;
            acc.HashPassword = _passwordHasher.HashPassword(acc, account.Password);

            return acc;
        }
    }
}
