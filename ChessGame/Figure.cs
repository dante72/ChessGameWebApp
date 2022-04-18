using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public abstract class Figure
    {
        public int MovesCount { get; set; }
        public FigureColors Color { get; private set; }
        public Cell? Position { get; set; }
        public abstract List<Cell> GetAllPossibleMoves();
        public Figure(FigureColors color)
        {
            Color = color;
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
