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

        public ChessGameController(ILogger<ChessGameController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}