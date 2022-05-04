using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal interface ICell
    {
        int Row { get; }
        int Column { get; }
        Figure? Figure { get; set; }
    }
}
