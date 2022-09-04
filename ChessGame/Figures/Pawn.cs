using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Figures
{
    internal class Pawn : Figure
    {
        public Pawn(FigureColors color) : base(color)
        {
        }
        internal override List<Cell> GetAllPossibleMoves()
        {
            var moves = new List<Cell>();

            if (Position is not null)
            {
                var direction = Color == FigureColors.White ? Directions.Up : Directions.Down;

                moves.AddRange(AddAttackFields(direction));
                moves.AddRange(AddForwardFields(direction));
            }

            return moves;
        }

        private List<Cell> AddAttackFields(Directions direction)
        {
            var attackFields = new List<Cell>();
            if (direction == Directions.Up)
            {
                attackFields.AddRange(Position.GetCellsInDirection(Directions.LeftUp, 1));
                attackFields.AddRange(Position.GetCellsInDirection(Directions.RightUp, 1));
            }
            else
            {
                attackFields.AddRange(Position.GetCellsInDirection(Directions.LeftDown, 1));
                attackFields.AddRange(Position.GetCellsInDirection(Directions.RightDown, 1));
            }

            return attackFields.Where(cell => cell.Figure != null && cell.Figure.Color != Color).ToList();
        }

        private List<Cell> AddForwardFields(Directions direction)
        {
            var moves = new List<Cell>();
            int range = IsFirstMove == 0 && (Position.Row == 1 || Position.Row == 6) ? 2 : 1;
            moves.AddRange(Position.GetCellsInDirection(direction, range).Where(i => i.Figure == null));

            return moves;
        }
        internal override Figure Clone() => new Pawn(Color);
    }
}
