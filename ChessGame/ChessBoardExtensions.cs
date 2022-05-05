using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class ChessBoardExtensions
    {
        public static ChessBoardDto MapChanges (this ChessBoard chessBoard)
        {
            var chessBoardDto = new ChessBoardDto();

            foreach (ChessCell cell in chessBoard.Cells)
            {
                if (cell.FigureName != cell.GetActualFigureName())
                    chessBoardDto.Cells.Add (new ChessCellDto() { FigureName = cell.GetActualFigureName(), Row = cell.Row, Column = cell.Column });
            }

            return chessBoardDto;
        }
    }
}
