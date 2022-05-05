using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Board
    {
        internal Cell[,] Cells

        public Figure? this[int row, int column]
        {
            get => Cells[row, column].Figure;
            set => Cells[row, column].Figure = value;
        }
        public Board()
        {
            Cells = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    Cells[i, j] = new Cell(i, j, this);

            this[0, 0] = new Bishop(FigureColors.White);
        }
    }
}
