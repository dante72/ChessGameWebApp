using AuthService.Services;
using ChessWebAPI;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        public AuthController(IRegistrationService registrationService)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
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
            acc.Password = account.Password;

            return acc;
        }
    }
}
