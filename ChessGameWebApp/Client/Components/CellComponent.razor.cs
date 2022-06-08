﻿using ChessGame;
using ChessGameWebApp.Client.Services;
using ChessWebAPI;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ChessGameWebApp.Client.Components
{
    public class CellComponentModel : ComponentBase, IChessCellObserver
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
        
        private ChessCell _chessCell;
        [Parameter]
        public ChessCell ChessCell { 
            get => _chessCell;
            set
            {
                _chessCell = value;
                _chessCell.Subscribe(this);
            } 
        }
        [Parameter]
        public string? FigureName { get; set; }
        public ChessBoard Board { get => ParentComponent.Board; }
        public async void Click()
        {
            try
            {
                if (ParentComponent._GameHubService.IsConnected)
                {
                    await Board.Click(Row, Column);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }
        }
        public void Update()
        {
            FigureName = ChessCell.FigureName;
            IsMarked = ChessCell.IsMarked;
            IsPointer = ChessCell.IsPointer;
            StateHasChanged();
        }
        protected override void OnInitialized()
        {
            ParentComponent.Children.Add((CellComponent)this);
        }
    }
}
