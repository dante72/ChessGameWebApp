using ChessGame;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ChessGameWebApp.Client.Components
{
    public class CellComponentModel : ComponentBase
    {
        [Parameter]
        public GameComponent ParentComponent { get; set; }
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]

        public ILogger<CellComponentModel> logger { get; set; }
        public List<CellDto> PossibleMoves { get; set; }
        [Parameter]
        public bool IsMarked { get; set; } = false;
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public Figure? Figure { get; set; }

        public async void Click()
        {
            //IsMarked = true;
            try
            {
                PossibleMoves = await Http.GetFromJsonAsync<List<CellDto>>($"chessgame/possible_moves?row={Row}&column={Column}");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }
            ParentComponent.ClearMarks();
            ParentComponent.CreateMarks(PossibleMoves);
        }

        public void Click11()
        {
            IsMarked = true;
            StateHasChanged();
        }

        public void Clear()
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
