using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class CellExtension
    {
        public static CellDto Map(this Cell cell)
        {
            return new CellDto() { Column = cell.Column, Row = cell.Row, Figure = cell.Figure };
        }
    }
}
