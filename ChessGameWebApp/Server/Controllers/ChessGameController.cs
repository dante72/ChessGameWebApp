using ChessGame;
using ChessGameWebApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public IBoardViewModel Board()
        {
            _logger.LogInformation("Get Board");
            return _gameService.GetBoard();
        }

        [HttpGet("possible_moves")]
        public IEnumerable<CellDto> PossibleMoves(int row, int column)
        {
            return _gameService.GetPossibleMoves(row, column).Select(i => i.Map());
        }
    }
}
