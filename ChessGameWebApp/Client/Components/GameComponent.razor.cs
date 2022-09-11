﻿using ChessGame;
using ChessGameWebApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GameComponentModel : ComponentBase, IDisposable, IChessObserver
    {
        public bool Inversion { get => Board.PlayerColor == FigureColors.Black; }
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

        protected override void OnInitialized()
        {
            ((IChessObservable)Board).Subscribe(this);
        }

        public void Dispose()
        {
            ((IChessObservable)Board).Remove(this);
        }

        public void Update()
        {
            StateHasChanged();
        }
    }
}
