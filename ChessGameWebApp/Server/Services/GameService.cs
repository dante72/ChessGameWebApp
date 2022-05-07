using ChessGame;
using System.Text.Json;

namespace ChessGameWebApp.Server.Services
{
    internal class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly ChessBoard _board;
        public GameService(ILogger<GameService> logger, ChessBoard board)
        {
            _logger = logger;
            _board = board;
        }
        public ChessBoard GetBoard()
        {
            _logger.LogInformation("Get Board");
            //using (FileStream fs = new FileStream("board.json", FileMode.OpenOrCreate))
            //{
            //    JsonSerializer.Serialize(fs, _board);
            //}
            return _board;
        }

        public List<Cell> GetPossibleMoves(int row, int column)
        {
            if (_board[row, column] is not null)
            {
                var res  = _board[row, column].GetAllPossibleMoves();
                //using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
                //{
                //    JsonSerializer.Serialize(fs, res);
                //}
                return res;
            }
            else
                return new List<Cell>();
        }

        public void Move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            var from = _board.GetCell(fromRow, fromColumn);
            var to = _board.GetCell(toRow, toColumn);
            from.Figure?.MoveTo(to);
        }
    }
}
