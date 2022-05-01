using ChessGame;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class CellComponentModel : ComponentBase
    {
        [Parameter]
        public bool IsMarked { get; set; } = false;
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public Figure? Figure { get; set; }

        public void Click()
        {
            IsMarked = true;
        }
    }
}
