using ChessGame;
using ChessGameWebApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class StatusComponentModel : ComponentBase, IChessObserver
    {
        public IPlayer Player { get; set; }
        public IPlayer Opponent { get; set; }
        [Parameter]
        public string Status { get; set; }
        [Inject]
        public IGameHubService _GameHubService { get; set; }
        private ChessBoard board;
        [Parameter]
        public ChessBoard Board
        {
            get => board;
            set
            {
                board = value;
            }
        }
        public async void Update()
        {
            switch(Board.GameStatus)
            {
                case GameStatus.Normal:
                    Status = Board.GetCurrentPlayer() == Board.Player.Color ? "Ваш ход" : "Ход соперника";
                    break;
                case GameStatus.Check:
                    Status = Board.GetCurrentPlayer() == Board.Player.Color ? "Соперник объявил вам ШАХ!" : "Вы объявили ШАХ сопернику!";
                    break;
                case GameStatus.Stalemate:
                    Status = "Пат, Ничья!";
                    break;
                case GameStatus.Checkmate:
                    Status = Board.GetCurrentPlayer() == Board.Player.Color ? "Соперник объявил вам МАТ!" : "Вы объявили МАТ сопернику!";
                    break;
                case GameStatus.TimeIsUp:
                    Status = Board.GetCurrentPlayer() == Board.Player.Color ? "Ваше время вышло!" : "Время соперника вышло!";
                    await _GameHubService.GameOver();
                    break;
            }

            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            if (Board.Player != null && Board.Players != null)
            {
                Player = (Player)Board.Player;
                Opponent = (Player)Board.Players.First(p => p != Player);
            }
            ((IChessObservable)Board).Subscribe(this);
        }

        public void Dispose()
        {
            ((IChessObservable)Board).Remove(this);
        }
    }
}
