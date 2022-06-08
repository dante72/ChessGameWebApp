using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using ChessGame.Figures;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessGameController : ControllerBase
    {
        private readonly ILogger<ChessGameController> _logger;
        private readonly IServerGameService _gameService;

        public ChessGameController(ILogger<ChessGameController> logger, IServerGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }
    }
}