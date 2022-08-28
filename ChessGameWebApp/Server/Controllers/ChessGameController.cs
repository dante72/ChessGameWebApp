using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessGameController : Controller
    {
        private readonly ILogger<ChessGameController> _logger;
        private readonly IPlayerService _queueService;

        public ChessGameController(ILogger<ChessGameController> logger, IPlayerService queueService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
        }
        [HttpGet("GetUser")]
        [Authorize]
        public Task<UserInfo> GetUser()
        {
            var siteUser = new UserInfo();
            var claims = User.Identity as ClaimsIdentity;

            siteUser.UserName = claims.FindFirst(ClaimTypes.Email)?.Value;
            siteUser.AccountId = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Task.FromResult(siteUser);
        }

        
        [HttpGet ("AddPlayer")]
        [Authorize]
        public async Task AddPlayer()
        {
            var claims = User.Identity as ClaimsIdentity;
            int id = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Player player = new Player() { Id = id };
            await _queueService.Add(player);
        }

        
        [HttpGet("PlayerCount")]
        [Authorize]
        public async Task<int> PlayerCount()
        {
            return await _queueService.Count();
        }
    }
}