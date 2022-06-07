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
        public ILogger<CellComponentModel> logger { get; set; }
        [Parameter]
        public bool IsMarked { get; set; }
        [Parameter]
        public bool IsPointer { get; set; }
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
                await Board.Click(Row, Column);
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
            IsPointer = ParentComponent.Board.GetCell(Row, Column).IsPointer;
            StateHasChanged();
        }
        protected override void OnInitialized()
        {
            ParentComponent.Children.Add((CellComponent)this);
        }
    }
}
