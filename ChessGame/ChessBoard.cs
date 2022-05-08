using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessBoard : Board
    {
        internal Cell Target 
        {
            get 
            {
                foreach (ChessCell cell in Cells)
                    if (cell.IsTarget)
                        return cell;

                return null;
            }
        }
        public ChessBoard(bool setup = false)
        {
            Cells = new ChessCell[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Cells[i, j] = new ChessCell(i, j, this);

            if (setup)
                Setup();
        }

        public ChessCell GetCell(int row, int column)
        {
            return (ChessCell)Cells[row, column];
        }
        private void ClearPossibleMoves()
        {
            foreach (ChessCell cell in Cells)
                cell.IsMarked = false;
        }

        public void ShowPossibleMoves(IEnumerable<Cell> cells)
        {
            ClearPossibleMoves();

            if (cells != null)
            foreach (Cell cell in cells)
                GetCell(cell.Row, cell.Column).IsMarked = true;
        }

        public void TargetClear()
        {
            foreach (ChessCell cell in Cells)
            {
                if (cell.IsTarget == true)
                {
                    cell.IsTarget = false;
                    break;
                }
            }
        }

        public void Click(int row, int column)
        {
            
            var cell = GetCell(row, column);

            if (cell.IsMarked)
            {
                Target.Figure.MoveTo(cell);
                ClearPossibleMoves();
            }
            else
            {
                ShowPossibleMoves(cell.Figure?.GetAllPossibleMoves());
            }
            
            TargetClear();
            cell.IsTarget = true;
        }
    }
}
