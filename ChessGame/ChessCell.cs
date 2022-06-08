using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{        
    /// <summary>
    /// Класс для клиет-серверного взаимодействия
    /// </summary>
    public class ChessCell : Cell
    {
        public bool IsPointer { get; set; }
        public bool IsMarked { get; set; }
        public string? FigureName { get; set; }
        public ChessCell(int row, int column, Board board) : base(row, column, board) { }
        private string? GetActualFigureName()
        {
            if (Figure == null)
                return null;

            return $"{Figure.Color}{Figure.GetType().Name}";
        }

        public override Figure? Figure
        {
            get { return base.Figure; }

            set { base.Figure = value; FigureName = GetActualFigureName(); }
        }
    }
}
