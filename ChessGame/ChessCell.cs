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
    public class ChessCell : Cell, IChessCellObservable
    {
        private IChessCellObserver? observer;
        private bool _isPointer;
        public bool IsPointer { get => _isPointer; set { _isPointer = value; observer?.Update(); } }
        private bool _isMarked;
        public bool IsMarked { get => _isMarked; set { _isMarked = value; observer?.Update(); } }

        private string? _figureName;
        public string? FigureName { get => _figureName; set { _figureName = value; observer?.Update(); } }
        public ChessCell(int row, int column, Board board) : base(row, column, board) { }
        private string? GetActualFigureName()
        {
            if (Figure == null)
                return null;

            return $"{Figure.Color}{Figure.GetType().Name}";
        }

        public void Subscribe(IChessCellObserver observer)
        {
            this.observer = observer ?? throw new ArgumentNullException(nameof(observer));  
        }

        public override Figure? Figure
        {
            get { return base.Figure; }

            set { 
                base.Figure = value;
                FigureName = GetActualFigureName();
            }
        }
    }
}
