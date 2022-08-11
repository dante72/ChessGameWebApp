using ChessGame;
using ChessGameWebApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase
    {
        [Inject]
        public IClientGameService _ClientGameService { get; set; }

        [Inject]
        public IGameHubService _GameHubService { get; set; }
        public CellComponentModel Target { get; set; }
        [Inject]
        public ChessBoard Board { get; set; }
        public List<CellComponent> Children { get; set; }
        public GameComponentModel()
        {
            Children = new List<CellComponent>();
        }

        protected override async Task OnInitializedAsync()
        {
            await _GameHubService.GetBoard();
        }
    }
}
