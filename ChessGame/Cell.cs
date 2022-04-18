using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }
        public Figure? Figure { get; set; }
        public Board Board { get; }
        public Cell(int row, int column, Board board)
        {
            Row = row;
            Column = column;
            Board = board;
        }
    }
}
