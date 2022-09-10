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
            switch(Board.GameStatus)
            {
                case GameStatus.Normal:
                    Status = Board.GetCurrentPlayer() == Board.PlayerColor ? "Ваш ход" : "Ход соперника";
                    break;
                case GameStatus.Check:
                    Status = Board.GetCurrentPlayer() == Board.PlayerColor ? "Соперник объявил вам ШАХ!" : "Вы объявили ШАХ сопернику!";
                    break;
                case GameStatus.Stalemate:
                    Status = "Пат, Ничья";
                    break;
                case GameStatus.Checkmate:
                    Status = Board.GetCurrentPlayer() == Board.PlayerColor ? "Соперник объявил вам МАТ!" : "Вы объявили МАТ сопернику!";
                    break;
            }

            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            Update();
            base.OnInitialized();
        }
    }
}
