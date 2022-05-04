using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Board : IBoard
    {
        internal Cell[,] cells;

        public Figure? this[int row, int column]
        {
            get => cells[row, column].Figure;
            set => cells[row, column].Figure = value;
        }

        public Cell GetCell(int row, int column)
        {
            return cells[row, column];
        }
        public Board()
        {
            cells = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    cells[i, j] = new Cell(i, j, this);

            this[0, 0] = new Bishop(FigureColors.White);
        }
    }
}
