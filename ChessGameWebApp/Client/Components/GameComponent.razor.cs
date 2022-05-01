using ChessGame;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase
    {
        public BoardViewModel? Board { get; set; }
    }
}
