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
        private bool isMarked;
        [Parameter]
        public bool IsMarked { get => isMarked; set { isMarked = value; StateHasChanged(); } }
        private bool isTarget;
        [Parameter]
        public bool IsTarget
        {
            get { isTarget = ParentComponent.Target == this; return isTarget; }
            set { isTarget = value; StateHasChanged(); }
        }
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public Figure? Figure { get; set; }
        private string? figureName;
        [Parameter]
        public string? FigureName { get => figureName; set { figureName = value; StateHasChanged(); } }

        public async void Click()
        {
            try
            {
                
                if (IsMarked)
                {
                    var target = ParentComponent.Children.First(i => i.IsTarget);
                    await webApi.Move(target.Row, target.Column, Row, Column, ParentComponent.Board);
                    
                    ParentComponent.ClearMarks();
                    ParentComponent.Target = this;
                    ParentComponent.Update();
                }
                else
                {
                    ParentComponent.Target = this;
                    PossibleMoves = await webApi.GetPossibleMovesAsync(Row, Column);
                    ParentComponent.ClearMarks();
                    ParentComponent.CreateMarks(PossibleMoves);
                }
                
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }


        }

        /*public void Marking()
        {
            IsMarked = true;
            StateHasChanged();
        }*/

        /*public void Unmarking()
        {
            IsMarked = false;
            StateHasChanged();
        }*/

        public void Update()
        {
            FigureName = ParentComponent.Board.GetCell(Row, Column).FigureName;
        }

        protected override void OnInitialized()
        {
            ParentComponent.Children.Add((CellComponent)this);
        }

    }
}
