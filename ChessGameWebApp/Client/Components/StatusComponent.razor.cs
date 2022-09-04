using ChessGame;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class StatusComponentModel : ComponentBase, IChessObserver
    {
        [Parameter]
        public string Status { get; set; }
        private ChessBoard board;
        [Parameter]
        public ChessBoard Board
        {
            get => board;
            set
            {
                board = value;
                Board.Subscribe(this);
            }
        }
        public void Update()
        {
            Status = Enum.GetName(Board.GameStatus);
            StateHasChanged();
        }
    }
}
