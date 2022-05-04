using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class CellExtension
    {
        public static ChessCellDto Map(this Cell cell)
        {
            return new ChessCellDto() { Column = cell.Column, Row = cell.Row, Figure = cell.Figure };
        }

        public static ChessCell Map(this ChessCellDto cell)
        {
            return new ChessCell(cell.Row, cell.Column, null) { Figure = cell.Figure };
        }

        public static ChessCellDto[,] Map(this Cell[,] cells)
        {
            ChessCellDto[,] array = new ChessCellDto[cells.GetLength(0), cells.GetLength(1)];
            for (int  i= 0; i < cells.GetLength(0); i++)
            {
                for (int j= 0; j < cells.GetLength(1); j++)
                {
                    array[i, j] = cells[i, j].Map();
                }
            }

            return array;
        }

    }
}
