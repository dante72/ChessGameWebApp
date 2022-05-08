using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Figures
{
    internal class King : Figure
    {
        public King(FigureColors color) : base(color)
        {
        }
        public override List<Cell> GetAllPossibleMoves()
        {
            var list = new List<Cell>();
            if (Position is not null)
            {
                for (int i = Position.Row - 1; i <= Position.Row + 1; i++)
                    for (int j = Position.Column - 1; j <= Position.Column + 1; j++)
                        if (i >= 0 && j >= 0 && i < 8 && j < 8 && !(i == Position.Row && j == Position.Column))
                            list.Add(Board.Cells[i, j]);
            }

            return list;
        }
    }
}
