using ChessGame;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class CellComponentModel : ComponentBase
    {
        public bool IsMarked { get; set; }
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public Figure? Figure { get; set; }
    }
}
