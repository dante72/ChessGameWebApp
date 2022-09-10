using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Board : IEnumerable<Cell>
    {
        internal Stack<Figure> MovedFigures { set; get; } = new Stack<Figure>();
        public FigureColors IsAllowedMove { get => Index % 2 == 0 ? FigureColors.White : FigureColors.Black; }
        internal int Index { get; set; } = 0;
        internal Cell[,] Cells;
        internal Figure? this[int row, int column]
        {
            get => Cells[row, column].Figure;
            set => Cells[row, column].Figure = value;
        }
        public virtual GameStatus GameStatus { get; set; }
        public Board(bool setup = false)
        {
            Cells = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    Cells[i, j] = new Cell(i, j, this);

            if (setup)
                Setup();
        }
        internal Board(Board copy)
        {
            Index = copy.Index;
            Cells = Cells = new Cell[8, 8];

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var cell = new Cell(i, j, this);
                    var figure = copy[i, j]?.Clone();

                    if (figure != null)
                        cell.Figure = figure;
                    
                    Cells[i, j] = cell;
                }
        }

        public Cell GetCell(int row, int column)
        {
            return Cells[row, column];
        }
        internal void Setup()
        {
            this[0, 0] = new Rook(FigureColors.Black);
            this[0, 1] = new Knight(FigureColors.Black);
            this[0, 2] = new Bishop(FigureColors.Black);
            this[0, 3] = new Queen(FigureColors.Black);
            this[0, 4] = new King(FigureColors.Black);
            this[0, 5] = new Bishop(FigureColors.Black);
            this[0, 6] = new Knight(FigureColors.Black);
            this[0, 7] = new Rook(FigureColors.Black);
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(FigureColors.Black);
                this[6, i] = new Pawn(FigureColors.White);
            }
            this[7, 0] = new Rook(FigureColors.White);
            this[7, 1] = new Knight(FigureColors.White);
            this[7, 2] = new Bishop(FigureColors.White);
            this[7, 3] = new Queen(FigureColors.White);
            this[7, 4] = new King(FigureColors.White);
            this[7, 5] = new Bishop(FigureColors.White);
            this[7, 6] = new Knight(FigureColors.White);
            this[7, 7] = new Rook(FigureColors.White);
        }

        public void TryMoveBack()
        {
            MoveBack();
            GameStatus = GetGameStatus();
        }

        internal void MoveBack()
        {
            if (MovedFigures.Count == 0)
                return;

            var last = MovedFigures.Pop();
            int index = last.CheckBoardIndex();

            last.MoveBack();
            
            if (MovedFigures.Count > 0 && index == MovedFigures.Peek().CheckBoardIndex())
                MovedFigures.Pop().MoveBack(true);

            Index--;
        }

        public void TryMove(Cell from, Cell to)
        {
            if (from.Figure?.PossibleMoves.Contains(to) != true)
                throw new InvalidOperationException("Error! Such step is impossible");

            from.Figure.MoveTo(to);
            GameStatus = GetGameStatus();
        }

        internal void Setup1()
        {
            this[0, 0] = new Rook(FigureColors.Black);
            this[0, 4] = new King(FigureColors.Black);
            this[0, 7] = new Rook(FigureColors.Black);

            this[7, 0] = new Rook(FigureColors.White);

            this[7, 4] = new King(FigureColors.White);
            this[7, 7] = new Rook(FigureColors.White);
        }

        internal bool IsCheckToKing(FigureColors color)
        {
            var cell = this.First(cell => cell.Figure is King king && king.Color == color);

            return IsUnderAttack(cell, color == FigureColors.White ? FigureColors.Black : FigureColors.White);
        }

        internal bool IsLastMove(FigureColors color)
        {
            return !this
                .Where(cell => cell.Figure != null)
                .Select(cell => cell.Figure)
                .Where(figure => figure.Color == color)
                .SelectMany(figure => figure.PossibleMoves)
                .Any();
        }

        internal bool IsUnderAttack(Cell cell, FigureColors color)
        {
            return this
                .Where(cell => cell.Figure != null)
                .Select(cell => cell.Figure)
                .Where(figure => figure.Color == color)
                .SelectMany(figure => figure.GetAllPossibleMoves())
                .Any(move => move == cell);
        }

        public GameStatus GetGameStatus()
        {
            FigureColors player = GetCurrentPlayer();

            bool lastMove = IsLastMove(player);
            bool check = IsCheckToKing(player);

            if (check && lastMove)
                return GameStatus.Checkmate;

            if (lastMove)
                return GameStatus.Stalemate;

            if (check)
                return GameStatus.Check;

            return GameStatus.Normal;
        }
        public FigureColors GetCurrentPlayer() => Index % 2 == 0 ? FigureColors.White : FigureColors.Black;
        public IEnumerator<Cell> GetEnumerator() => Cells.Cast<Cell>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Cells.GetEnumerator();
    }
}
