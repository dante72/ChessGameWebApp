using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class BoardViewModel : Board, IBoardViewModel
    {
        public BoardViewModel()
        {
            cells = new CellViewModel[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    cells[i, j] = new CellViewModel(i, j, this);

            this[0, 0] = new Bishop(FigureColors.White);
        }
    }
}
