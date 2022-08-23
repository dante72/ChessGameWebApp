using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using ChessGame.Figures;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ChessGameWebApp.Shared;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessGameController : Controller
    {
        private readonly ILogger<ChessGameController> _logger;
        private readonly IQueueService _queueService;

        public ChessGameController(ILogger<ChessGameController> logger, IQueueService queueService)
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

        
        [HttpGet (Name ="AddPlayer")]
        [Authorize]
        public async Task AddPlayer(int id)
        {
            Player player = new Player() { Id = id };
            await _queueService.Add(player);
        }

        
        [HttpGet(Name = "PlayerCount")]
        [Authorize]
        public async Task<int> PlayerCount()
        {
            return await _queueService.Count();
        }
    }
}