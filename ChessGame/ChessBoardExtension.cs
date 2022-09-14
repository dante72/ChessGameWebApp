using ChessGame.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class ChessBoardExtension
    {
        public static ChessBoardDto ToDto(this ChessBoard chessBoard)
        {
            var chessBoardDto = new ChessBoardDto();

            foreach (ChessCell cell in chessBoard.Cells)
            {
                chessBoardDto.Cells.Add(new ChessCellDto() {
                    Row = cell.Row, Column = cell.Column, 
                    IsMarked = cell.IsMarked, 
                    IsTarget = cell.IsPointer,
                    Figure = cell.Figure?.ToDto()
                });
            }

            chessBoardDto.Index = chessBoard.Index;

            return chessBoardDto;
        }

        public static ChessBoardDto ToDto(this Board chessBoard)
        {
            var chessBoardDto = new ChessBoardDto();

            foreach (Cell cell in chessBoard.Cells)
            {
                chessBoardDto.Cells.Add(new ChessCellDto()
                {
                    Row = cell.Row,
                    Column = cell.Column,
                    Figure = cell.Figure?.ToDto()
                });
            }

            chessBoardDto.Index = chessBoard.Index;

            return chessBoardDto;
        }

        public static void Update(this ChessBoard chessBoard, ChessBoardDto? data)
        {
            if (data == null)
                return;
            foreach (var cell in data.Cells)
            {
                chessBoard[cell.Row, cell.Column] = data.Cells[cell.Row * 8 + cell.Column].Figure?.FromDto();
            }
            chessBoard.Index = data.Index;
        }

        public static ChessBoard FromDto(this ChessBoardDto data)
        {
            var board = new ChessBoard();
            int index = 0;
            foreach (var cell in data.Cells)
            {
                board[index % 8, index / 8] = cell.Figure?.FromDto();
                index++;
            }

            return board;
        }

        public static ChessTimerDto GetTimer(this Board board)
        {
            if (board.Players == null)
                return null;

            var timerDto = new ChessTimerDto();
            foreach (var player in board.Players)
            {
                timerDto.Players
                    .First(p => p.Color == player.Color)
                    .Delta = player.Timer.Delta;
            }

            return timerDto;
        }

        public static void SetTimer(this ChessBoard board, ChessTimerDto timer)
        {
            if (board.Players == null)
                return;

            foreach (var player in board.Players)
            {
                player.Timer.Delta = timer.Players.First(p => p.Color == player.Color).Delta;
            }
        }

        public static FigureDto? ToDto(this Figure? figure)
        {
            if (figure == null)
                return null;
            return new FigureDto()
            {
                IsFirstMove = figure.IsFirstMove,
                Color = figure.Color,
                Type = figure.GetType().Name
            };
        }

        public static Figure? FromDto(this FigureDto? figure)
        {
            if (figure == null)
                    return null;
            switch (figure.Type)
            {
                case "Pawn":
                    return new Pawn(figure.Color) { IsFirstMove = figure.IsFirstMove };
                case "Bishop":
                    return new Bishop(figure.Color) { IsFirstMove = figure.IsFirstMove };
                case "Knight":
                    return new Knight(figure.Color) { IsFirstMove = figure.IsFirstMove };
                case "Queen":
                    return new Queen(figure.Color) { IsFirstMove = figure.IsFirstMove };
                case "Rook":
                    return new Rook(figure.Color) { IsFirstMove = figure.IsFirstMove };
                case "King":
                    return new King(figure.Color) { IsFirstMove = figure.IsFirstMove };
                default:
                    throw new ArgumentOutOfRangeException("Нет такой фигуры");
            }
        }
    }
}
