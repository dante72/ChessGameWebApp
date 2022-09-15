using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PlayerDto
    {
        public FigureColors Color { get; set; }
        public TimeSpan Delta { get; set; }
        public bool TurnOn { get; set; }
    }
}
