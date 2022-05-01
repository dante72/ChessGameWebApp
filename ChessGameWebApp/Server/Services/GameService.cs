﻿using ChessGame;

namespace ChessGameWebApp.Server.Services
{
    internal class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IBoardViewModel _board;
        public GameService(ILogger<GameService> logger, IBoardViewModel board)
        {
            _logger = logger;
            _board = board;
        }
        public IBoardViewModel GetBoard()
        {
            _logger.LogInformation("Get Board");
            return _board;
        }

        /*public IList<Cell> GetPossibleMoves(int row, int column)
        {
            if (Board[row, column].Figure is not null)
                return cell.Figure.GetAllPossibleMoves();
            else
                return new List<Cell>();
        }*/

        public IList<Cell> GetPossibleMoves(Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}