﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class CellDto
    {
        public Figure? Figure { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public CellDto() { }
    }
}
