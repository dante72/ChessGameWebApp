using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessTimerDto
    {
        public PlayerDto[] Players { get; set; }

        public ChessTimerDto()
        {
            Players = new PlayerDto[2];
            Players[0] = new PlayerDto() { Color = FigureColors.White };
            Players[1] = new PlayerDto() { Color = FigureColors.Black };
        }
    }

    public class PlayerDto
    {
        public FigureColors Color { get; set; }
        public TimeSpan Delta { get; set; }
    }
}