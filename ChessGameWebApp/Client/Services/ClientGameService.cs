using ChessGame;
using ChessWebAPI;

namespace ChessGameWebApp.Client.Services
{
    public class ClientGameService : IClientGameService
    {
        private readonly ILogger<ClientGameService> _logger;
        private readonly ChessBoard _board;
        private readonly ServerWebApi _web;
        private bool updateFromServer = true;
        public ClientGameService(ILogger<ClientGameService> logger, ChessBoard board, ServerWebApi webApi)
        {
            _logger = logger;
            _board = board;
            _web = webApi;
        }
        public async Task GetBoard()
        {
            if (updateFromServer)
            {
                updateFromServer = false;
                await _web.GetBoard(_board);
            }
                
            _board.Update();
        }

        public void BoardUpdateFromServer()
        {
            updateFromServer = true;
        }

        public async Task<ChessCellDto> GetCell()
        {
            return await _web.GetCell();
        }

        public async Task<Figure?> GetFigure()
        {
            var f = await _web.GetFigure();
            return f.FromDto();
        }

        public async Task<ChessBoard> GetBoard2()
        {
            var t = await _web.GetBoard2();
            return t;
        }
    }
}
