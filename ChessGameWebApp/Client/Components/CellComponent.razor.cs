using ChessGame;
using ChessWebAPI;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ChessGameWebApp.Client.Components
{
    public class CellComponentModel : ComponentBase
    {
        [Parameter]
        public GameComponent ParentComponent { get; set; }
        [Inject]
        public WebApi webApi { get; set; }
        [Inject]
        public ILogger<CellComponentModel> logger { get; set; }
        [Parameter]
        public bool IsMarked { get; set; }
        [Parameter]
        public bool IsTarget { get; set; }
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public string? FigureName { get; set; }
        public ChessBoard Board { get => ParentComponent.Board; }
        public async void Click()
        {
            try
            {
                await webApi.Click(Row, Column, ParentComponent.Board);
                ParentComponent.Update();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }
        }

        public void Update()
        {
            FigureName = ParentComponent.Board.GetCell(Row, Column).FigureName;
            IsMarked = ParentComponent.Board.GetCell(Row, Column).IsMarked;
            IsTarget = ParentComponent.Board.GetCell(Row, Column).IsTarget;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ParentComponent.Children.Add((CellComponent)this);
        }
    }
}
