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
        internal Stack<SavedMove> SavedMoves { set; get; } = new Stack<SavedMove>();
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

        public void TryMoveTo(Cell cell)
        {
            if (!GetPossibleMoves().Contains(cell))
                throw new InvalidOperationException("Error! Such step is impossible");

            MoveTo(cell);
        }
        internal virtual void MoveTo(Cell cell, bool doubleMove = false)
        {
            cell.Figure?.SaveMoves(cell);
            SaveMoves(Position);

            Position.Figure = null;
            cell.Figure = this;
            
            IsFirstMove++;
            if (!doubleMove)
                Board.Index++;
        }
        internal int CheckBoardIndex()
        {
            return SavedMoves.Peek().BoardIndex;
        }

        internal void MoveBack()
        {
            var savedMove = SavedMoves.Pop();
            Position.Figure = null;
            savedMove.Move.Figure = this;
            IsFirstMove--;
        }

        private void SaveMoves(Cell lastMove)
        {
            Board.MovedFigures.Push(this);
            SavedMoves.Push(new SavedMove()
            {
                Move = lastMove,
                BoardIndex = Board.Index
            });
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
