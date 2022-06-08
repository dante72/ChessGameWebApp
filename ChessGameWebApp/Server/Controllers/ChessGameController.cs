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

        [HttpGet("Board")]
        public ChessBoardDto Board()
        {
            _logger.LogInformation("Get Board");
            var b1 = _gameService.GetBoard();
            var b = b1.ToDto();
            return b;
        }

        [HttpGet("Board2")]
        public async Task<ChessBoardDto> Board2()
        {
            _logger.LogInformation("Get Board");
            var b1 = _gameService.GetBoard();
            return b1.ToDto();
        }
        [HttpGet("Cell")]
        public async Task<Cell> Cell()
        {
            return new Cell(1, 1, null);
        }

        [HttpGet("Move")]
        public bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            return _gameService.TryMove(fromRow, fromColumn, toRow, toColumn);
        }

        [HttpGet("Figure")]
        public async Task<FigureDto?> Figure()
        {
            var dd = new Bishop(FigureColors.Black) as Figure;
            var tt = dd.ToDto();
            return tt;
        }

        //[HttpGet("click")]
        //public ChessBoardDto Click(int row, int column)
        //{
        //    return _gameService.Click(row, column).ToDto();
        //}
    }
}