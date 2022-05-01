using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class CellViewModel : Cell
    {
        public bool IsMarked { get; set; }

        public new string Figure { get; set; }
        public CellViewModel(int row, int column, Board board) : base(row, column, board)
        {
        }
    }
}
