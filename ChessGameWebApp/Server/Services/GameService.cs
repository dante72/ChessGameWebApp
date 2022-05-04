using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    internal class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IChessBoard _board;
        public GameService(ILogger<GameService> logger, IChessBoard board)
        {
            _logger = logger;
            _board = board;
        }
        public IChessBoard GetBoard()
        {
            _logger.LogInformation("Get Board");
            return _board;
        }

        public List<Cell> GetPossibleMoves(int row, int column)
        {
            if (_board[row, column] is not null)
                return _board[row, column].GetAllPossibleMoves();
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
