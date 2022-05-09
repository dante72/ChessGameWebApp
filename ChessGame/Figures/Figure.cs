using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChessGame
{
    internal abstract class Figure
    {
        internal int MovesCount { get; set; }
        internal FigureColors Color { get; private set; }
        internal Cell Position { get; set; }
        public abstract IEnumerable<Cell> GetAllPossibleMoves();
        internal Figure(FigureColors color)
        {
            Color = color;
        }
        internal Board Board { get => Position.Board; }
        internal virtual void MoveTo(Cell cell)
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
