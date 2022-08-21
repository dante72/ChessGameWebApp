using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using ChessGame.Figures;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessGameController : ControllerBase
    {
        private readonly ILogger<ChessGameController> _logger;

        public ChessGameController(ILogger<ChessGameController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [Authorize]
        public UserInfo Get()
        {
            var siteUser = new UserInfo();
            var claims = User.Identity as ClaimsIdentity;

            siteUser.UserName = claims.FindFirst(ClaimTypes.Email)?.Value;
            siteUser.AccountId = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return siteUser;
        }

    }
}