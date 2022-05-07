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
            b1.UpdateFigureNames();
            return b;
        }

        [HttpGet("possible_moves")]
        public IEnumerable<ChessCellDto> PossibleMoves(int row, int column)
        {
            return _gameService.GetPossibleMoves(row, column).Select(i => i.ToDto());
        }

        [HttpGet("move")]
        public ChessBoardDto Move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            var b1 = _gameService.GetBoard();
            _gameService.Move(fromRow, fromColumn, toRow, toColumn);
            var b = b1.MapChanges();
            b1.UpdateFigureNames();
            return b;
        }
    }
}
