using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public interface IBoardViewModel
    {
        Figure? this[int row, int column] { get; set; }
    }
}
