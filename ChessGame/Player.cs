using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Player : IPlayer
    {
        public FigureColors Color { get; set; }
        public ChessTimer Timer { get; set; } = new ChessTimer();
    }
}
