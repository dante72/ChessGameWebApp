using DbContextDao;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthContext _dbContext;
        public AuthController(AuthContext dbContext)
        { 
            _dbContext = dbContext;
        }
        [HttpPost(Name = "Registaration")]
        public void Registration()
        {
            _dbContext.Users.Add(new Models.Account() { Email = "test2", Login = "hello", Username = "test", HashPassword="ttttt", IsBanned = false, Id = 0 });
            _dbContext.SaveChanges();
        }
    }
}
