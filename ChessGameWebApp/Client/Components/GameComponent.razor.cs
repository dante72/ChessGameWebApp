using ChessGame;
using ChessGameWebApp.Client.Services;
using ChessWebAPI;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase
    {
        [Inject]
        public IClientGameService ClientGameService { get; set; }
        public CellComponentModel Target { get; set; }
        [Inject]
        public ChessBoard Board { get; set; }
        public List<CellComponent> Children { get; set; }
        public GameComponentModel()
        {
            Children = new List<CellComponent>();
        }
        public void Update()
        {
            Children.ForEach(i => i.Update());
        }
        protected override async Task OnInitializedAsync()
        {
            await ClientGameService.GetBoard();
            //await base.OnInitializedAwait();

        }
        protected override void OnAfterRender(bool firstRender)
        {
            Update();
            base.OnAfterRender(firstRender);
        }
    }
}
