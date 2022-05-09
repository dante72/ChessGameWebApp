using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;

namespace ChessGameWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessGameController : ControllerBase
    {
        private readonly ILogger<ChessGameController> _logger;
        private readonly IGameService _gameService;

        public ChessGameController(ILogger<ChessGameController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet("Board")]
        public ChessBoardDto Board()
        {
            _logger.LogInformation("Get Board");
            var b1 = _gameService.GetBoard();
            var b = b1.MapChanges();
            return b;
        }

        [HttpGet("click")]
        public ChessBoardDto Click(int row, int column)
        {
            return _gameService.Click(row, column).MapChanges();
        }
    }
}