using ChessGame;
using ChessWebAPI;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase
    {
        [Inject]
        public WebApi webApi { get; set; }
        public CellComponentModel Target { get; set; }
        public ChessBoard Board { get; set; }
        public List<CellComponent> Children { get; set; }
        public CellComponentModel this[int row, int column]
        {
            get => Children.First(i => i.Row == row && i.Column == column);
        }
        public GameComponentModel()
        {
            Children = new List<CellComponent>();
        }
        public void ClearMarks()
        {
            Children.ForEach(i => i.IsMarked = false);
        }
        public void Update()
        {
            Children.ForEach(i => i.Update());
        }
        public void CreateMarks(IEnumerable<Cell> cells)
        {
            cells.ToList().ForEach(cell => this[cell.Row, cell.Column].IsMarked = true);
        }
    }
}
