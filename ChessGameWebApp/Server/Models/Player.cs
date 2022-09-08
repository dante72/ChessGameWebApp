using ChessGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWebApp.Server.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public FigureColors Color { get; set; }
    }
}
