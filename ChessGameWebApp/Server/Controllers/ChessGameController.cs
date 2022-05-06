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
            return _gameService.GetBoard().MapChanges();
        }

        [HttpGet("possible_moves")]
        public IEnumerable<Cell> PossibleMoves(int row, int column)
        {
            return _gameService.GetPossibleMoves(row, column);
        }

        [HttpGet("move")]
        public void Move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            _gameService.Move(fromRow, fromColumn, toRow, toColumn);
        }
    }
}
