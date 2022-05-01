using ChessGame;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase
    {
        public BoardViewModel? Board { get; set; }
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
            Children.ForEach(i => i.Clear());
        }

        public void CreateMarks(List<CellDto> cells)
        {
            cells.ForEach(cell => this[cell.Row, cell.Column].Click11());
        }
    }
}
