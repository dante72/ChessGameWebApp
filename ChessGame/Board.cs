using ChessGame.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Board
    {
        internal int Index { get; set; } = 0;
        internal Cell[,] Cells;
        internal Figure? this[int row, int column]
        {
            get => Cells[row, column].Figure;
            set => Cells[row, column].Figure = value;
        }
        internal Board()
        {
            Cells = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    Cells[i, j] = new Cell(i, j, this);
        }
        internal void Setup()
        {
            this[0, 0] = new Rook(FigureColors.Black);
            this[0, 1] = new Knight(FigureColors.Black);
            this[0, 2] = new Bishop(FigureColors.Black);
            this[0, 3] = new Queen(FigureColors.Black);
            this[0, 4] = new King(FigureColors.Black);
            this[0, 5] = new Bishop(FigureColors.Black);
            this[0, 6] = new Knight(FigureColors.Black);
            this[0, 7] = new Rook(FigureColors.Black);
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(FigureColors.Black);
                this[6, i] = new Pawn(FigureColors.White);
            }
            this[7, 0] = new Rook(FigureColors.White);
            this[7, 1] = new Knight(FigureColors.White);
            this[7, 2] = new Bishop(FigureColors.White);
            this[7, 3] = new Queen(FigureColors.White);
            this[7, 4] = new King(FigureColors.White);
            this[7, 5] = new Bishop(FigureColors.White);
            this[7, 6] = new Knight(FigureColors.White);
            this[7, 7] = new Rook(FigureColors.White);
        }
    }
}
