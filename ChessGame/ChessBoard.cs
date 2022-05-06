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
        public ChessBoard()
        {
            Cells = new ChessCell[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Cells[i, j] = new ChessCell(i, j, this);

                this[0, 0] = new Bishop(FigureColors.White);

        }
        public ChessBoard(bool flag) : this()
        {
            this[0, 0] = new Bishop(FigureColors.White);
        }

        public ChessCell GetCell(int row, int column)
        {
            return (ChessCell)Cells[row, column];
        }

        public void UpdateFigureNames()
        {
            foreach (ChessCell cell in Cells)
            {
                cell.FigureName = cell.GetActualFigureName();
            }
        }
    }
}
