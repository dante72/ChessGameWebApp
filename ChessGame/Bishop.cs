using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Bishop : Figure
    {
        public Bishop(FigureColors color) : base(color)
        {
        }

        public override List<Cell> GetAllPossibleMoves()
        {
            return new List<Cell>();
        }
    }
}
