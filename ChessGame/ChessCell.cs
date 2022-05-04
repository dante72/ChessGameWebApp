using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessCell : Cell
    {
        private Queue<Cell> changeSummary;
        public bool IsMarked { get; set; }

        private Figure? _figure;
        public new Figure? Figure
        {
            get => _figure;
            set
            {
                _figure = value;
                if (_figure != null)
                    _figure.Position = this;

                changeSummary.Enqueue(this);
            }
        }

        public ChessCell(int row, int column, Board board = null) : base(row, column, board)
        {
            changeSummary = new Queue<Cell>();
        }
    }
}
