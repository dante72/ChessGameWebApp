﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Figures
{
    internal class Rook : Figure
    {
        public Rook(FigureColors color) : base(color)
        {
        }
        protected override List<Cell> GetAllPossibleMoves()
        {
            var list = new List<Cell>();
            if (Position is not null)
            {
                list.AddRange(Position.GetCellsInDirection(Directions.Up));
                list.AddRange(Position.GetCellsInDirection(Directions.Right));
                list.AddRange(Position.GetCellsInDirection(Directions.Left));
                list.AddRange(Position.GetCellsInDirection(Directions.Down));
            }

            return list;
        }
    }
}