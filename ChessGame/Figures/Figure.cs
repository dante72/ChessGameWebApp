using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChessGame
{
    public abstract class Figure
    {
        internal int MovesCount { get; set; }
        public FigureColors Color { get; private set; }
        internal Cell? Position { get; set; }
        public abstract List<Cell> GetAllPossibleMoves();
        internal Figure(FigureColors color)
        {
            Color = color;
        }
        internal Board Board { get => Position?.Board; }
        public virtual void MoveTo(Cell cell)
        {
            Position.Figure = null;
            cell.Figure = this;
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
