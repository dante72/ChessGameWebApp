using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public interface IPlayer
    {
        FigureColors Color { get; set; }
        ChessTimer Timer { get; set; }
    }
}
