using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal interface IBoard
    {
        Figure? this[int row, int column] { get; set; }
        public Cell GetCell(int row, int column);
    }
}
