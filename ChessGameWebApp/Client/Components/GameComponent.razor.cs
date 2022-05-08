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
        public ChessBoard Board { get; set; } = new ChessBoard();
        public List<CellComponent> Children { get; set; }
        public GameComponentModel()
        {
            Children = new List<CellComponent>();
        }
        public void Update()
        {
            Children.ForEach(i => i.Update());
        }
    }
}
