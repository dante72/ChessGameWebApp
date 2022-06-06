using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChessGame
{
    public abstract class Figure
    {
        internal int MovesCount { get; set; }
        internal FigureColors Color { get; private set; }
        internal Cell Position { get; set; }
        protected abstract IEnumerable<Cell> GetAllPossibleMoves();
        public IEnumerable<Cell> PossibleMoves
        {
            get => GetPossibleMoves();
        }
        public int IsFirstMove { get; set; }
        internal Figure(FigureColors color, int firstMove = 0)
        {
            Color = color;
            IsFirstMove = firstMove;
        }
        internal Board Board { get => Position.Board; }
        internal virtual void MoveTo(Cell cell)
        {
            Position.Figure = null;
            cell.Figure = this;
            IsFirstMove++;
            Position.Board.Index++;
        }
        public bool IsMove()
        {
            return Color == FigureColors.Black && Board.Index % 2 != 0 || Color == FigureColors.White && Board.Index % 2 == 0;
        }
        protected virtual IEnumerable<Cell> GetPossibleMoves()
        {
            if (IsMove())
                return new List<Cell>();

            var moves = GetAllPossibleMoves().Where(i => i.Figure?.Color != Color).ToList();

            return moves;
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
