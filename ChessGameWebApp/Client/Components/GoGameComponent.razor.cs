using AuthWebAPI;
using ChessGame;
using ChessGameWebApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Components
{
    public class GoGameComponentModel : ComponentBase, IDisposable, IChessObserver
    {
        [Inject]
        public SiteUserInfo User { get; set; }
        [Inject]
        IGameHubService GameHubService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IAuthWebApi AuthWebApi { get; set; }
        [Parameter]
        public bool IsStandState { get; set; } = false;
        public async void StartGame()
        {
            if (await AuthWebApi.SessionExists())
                NavigationManager.NavigateTo("/game/start");
            else
                await GameHubService.AddOrRemovePlayer();
        }

        protected override async Task OnInitializedAsync()
        {
            //устанавливает первое подключение для корректной работы первого подключения с сервера
            if (!GameHubService.IsConnected)
                await GameHubService.StartGame();
        }

        protected override void OnInitialized()
        {
            ((IChessObservable)User).Subscribe(this);
        }

        public void Dispose()
        {
            ((IChessObservable)User).Remove(this);
        }

        public Task UpdateAsync()
        {
            StateHasChanged();

            return Task.CompletedTask;
        }
    }
}

