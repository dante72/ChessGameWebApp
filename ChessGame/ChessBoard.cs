using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessBoard : Board, IEnumerable<ChessCell>, IChessObservable
    {
        public FigureColors? PlayerColor { get; set; }
        public delegate Task<bool> CheckMove(Cell from, Cell to);
        private CheckMove CheckFigureMove { get; set; } = delegate { return Task.FromResult(true); };
        private ChessCell target;
        internal ChessCell Target 
        {
            get => target;
            set
            {
                if (target is not null)
                    target.IsPointer = false;
                target = value;
                target.IsPointer = true;
            }
        }
        private GameStatus gameStatus;
        public override GameStatus GameStatus
        {
            get
            {
                return gameStatus;
            }
            set
            {
                gameStatus = value;
                observer?.Update();
            }
        }
        private IChessObserver? observer;
        public ChessBoard(bool setup = false)
        {
            Cells = new ChessCell[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Cells[i, j] = new ChessCell(i, j, this);

            if (setup)
                Setup();
        }
        public new ChessCell GetCell(int row, int column)
        {
            return (ChessCell)Cells[row, column];
        }
        private void ClearPossibleMoves()
        {
            foreach (ChessCell cell in Cells)
                cell.IsMarked = false;
        }

        private void ShowPossibleMoves(IEnumerable<Cell>? cells)
        {
            ClearPossibleMoves();

            if (cells != null)
            foreach (Cell cell in cells)
                GetCell(cell.Row, cell.Column).IsMarked = true;
        }
        public async Task Click(int row, int column)
        {
            var currentCell = (ChessCell)Cells[row, column];

            if (currentCell.IsMarked)
            {
                if (await CheckFigureMove(target, currentCell))
                {
                    if (PlayerColor == null || PlayerColor == IsAllowedMove)
                        TryMove(Target, currentCell);
                }
                
                ClearPossibleMoves();
            }
            else
            {
                ShowPossibleMoves(currentCell.Figure?.PossibleMoves);
            }

            Target = currentCell;
        }

        public void SetCheckMethod(CheckMove checkMove)
        {
            CheckFigureMove = checkMove;
        }
        public void Subscribe(IChessObserver observer)
        {
            this.observer = observer ?? throw new ArgumentNullException(nameof(observer));
        }

        public new IEnumerator<ChessCell> GetEnumerator() => Cells.Cast<ChessCell>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Cells.GetEnumerator();
    }
}
