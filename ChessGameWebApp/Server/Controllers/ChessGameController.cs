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
        private readonly IPlayerService _playerService;
        private readonly IGameSessionService _gameSessionService;

        public ChessGameController(ILogger<ChessGameController> logger, IPlayerService playerService, IGameSessionService gameSessionService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
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
            int id = GetUserId(User);
            Player player = new Player() { Id = id };
            await _playerService.Add(player);
        }

        
        [HttpGet("PlayerCount")]
        [Authorize]
        public async Task<int> PlayerCount()
        {
            return await _playerService.Count();
        }

        [HttpGet("SessionExists")]
        [Authorize]
        public async Task<bool> SessionExists()
        {
            int accountId = GetUserId(User);
            var result = await _gameSessionService.FindSession(accountId);
            return result == null ? false : true;
        }

        private int GetUserId(ClaimsPrincipal user)
        {
            var claims = User.Identity as ClaimsIdentity;
            return int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}