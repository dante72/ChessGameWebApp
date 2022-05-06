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
        public IEnumerable<Cell> PossibleMoves { get; set; }
        [Parameter]
        public bool IsMarked { get; set; } = false;
        private bool isTarget;
        [Parameter]
        public bool IsTarget
        {
            get { isTarget = ParentComponent.Target == this; return isTarget; }
            set { isTarget = value; }
        }
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public Figure? Figure { get; set; }
        [Parameter]
        public string? FigureName { get; set; }

        public async void Click()
        {
            ParentComponent.Target = this;
            try
            {
                PossibleMoves = await webApi.GetPossibleMovesAsync(Row, Column);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }
            ParentComponent.ClearMarks();
            ParentComponent.CreateMarks(PossibleMoves);
        }

        public void Marking()
        {
            IsMarked = true;
            StateHasChanged();
        }

        public void Unmarking()
        {
            IsMarked = false;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ParentComponent.Children.Add((CellComponent)this);
        }

    }
}
