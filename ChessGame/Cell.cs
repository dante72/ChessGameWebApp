﻿using System;
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
        internal Board Board { get; }
        internal Cell(int row, int column, Board board)
        {
            Row = row;
            Column = column;
            Board = board;
        }

        /// <summary>
        /// Получить ячейки в данном направлении
        /// </summary>
        internal List<Cell> GetCellsInDirection(Directions direction, int range = 8)
        {
            int x, y;
            switch (direction)
            {
                case Directions.Up:
                    x = 0;
                    y = -1;
                    break;
                case Directions.Down:
                    x = 0;
                    y = 1;
                    break;
                case Directions.Left:
                    x = 1;
                    y = 0;
                    break;
                case Directions.Right:
                    x = -1;
                    y = 0;
                    break;
                case Directions.LeftUp:
                    x = 1;
                    y = -1;
                    break;
                case Directions.RightDown:
                    x = -1;
                    y = 1;
                    break;
                case Directions.LeftDown:
                    x = 1;
                    y = 1;
                    break;
                case Directions.RightUp:
                    x = -1;
                    y = -1;
                    break;

                default:
                    throw new NotImplementedException("Error Direction");
            }

            var list = new List<Cell>();

            for (int i = 1; i <= range; i++)
            {
                if (Column + i * x >= 0 && Column + i * x < 8 && Row + i * y >= 0 && Row + i * y < 8)
                {
                    list.Add(Board.cells[Row + i * y, Column + i * x]);
                    if (Board[Row + i * y, Column + i * x] != null)
                        break;
                }
            }

            return list;
        }

        public static bool operator == (Cell b1, Cell b2)
        {
            return b1.Column == b2.Column && b1.Row == b2.Row;
        }

        public static bool operator != (Cell b1, Cell b2)
        {
            return !(b1 == b2);
        }
    }
}
