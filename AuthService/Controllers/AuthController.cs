using AuthService.Services;
using DbContextDao;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        public AuthController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpPost(Name = "Registaration")]
        public void Registration()
        {
            //_dbContext.Users.Add(new Models.Account() { Email = "test2", Login = "hello", Username = "test", HashPassword="ttttt", IsBanned = false, Id = 0 });
            //_dbContext.SaveChanges();
            //_accountRepository.Add(new Models.Account() { Email = "test3", Login = "hello", Username = "test", HashPassword = "ttttt", IsBanned = false, Id = 0 });
            _registrationService.AddAccount(new Models.Account() { Email = "test5", Login = "hello", Username = "test", HashPassword = "ttttt", IsBanned = false, Id = 0 });
        }
    }
}
