using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class ChessBoardExtension
    {
        public static ChessBoardDto MapChanges(this ChessBoard chessBoard)
        {
            var chessBoardDto = new ChessBoardDto();

            foreach (ChessCell cell in chessBoard.Cells)
            {
               // if (cell.FigureName != cell.GetActualFigureName())
                    chessBoardDto.Cells.Add(new ChessCellDto() { FigureName = cell.GetActualFigureName(), Row = cell.Row, Column = cell.Column, IsMarked = cell.IsMarked, IsTarget = cell.IsTarget });
            }
            return chessBoardDto;
        }

        public static void Update(this ChessBoard chessBoard, ChessBoardDto data)
        {
            foreach (var cell in data.Cells)
            {
                ((ChessCell)chessBoard.Cells[cell.Row, cell.Column]).FigureName = cell.FigureName;
                ((ChessCell)chessBoard.Cells[cell.Row, cell.Column]).IsMarked = cell.IsMarked;
                ((ChessCell)chessBoard.Cells[cell.Row, cell.Column]).IsTarget = cell.IsTarget;
            }
        }

        public static ChessCellDto ToDto(this Cell cell)
        {
            return new ChessCellDto() { Row = cell.Row, Column = cell.Column };
        }

        public static Cell ToCell(this ChessCellDto cell)
        {
            return new Cell(cell.Row, cell.Column, null);
        }
    }
}
