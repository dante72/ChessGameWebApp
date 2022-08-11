using AuthService.Services;
using AuthWebAPI;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Authentication;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IRegistrationService registrationService, ILogger<AuthController> logger)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost(Name = "Registaration")]
        public async Task<IActionResult> Registration([FromBody]AccountRequestModel account)
        {
            try
            {
                await _registrationService.AddAccount(account.Map());
                return Ok("Registration is successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "Authentication")]
        public async Task<IActionResult> Authentication(string login, string password)
        {
            try
            {
                var result = await _registrationService.GetTokens(login, password);
                return Ok(result);
            }
            catch (AuthenticationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest("Unknown error!");
            }
        }
    }
}
