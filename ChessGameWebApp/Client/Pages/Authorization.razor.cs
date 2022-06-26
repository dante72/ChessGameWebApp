using Microsoft.AspNetCore.Components;

namespace ChessGameWebApp.Client.Pages
{
    public class AuthorizationModel : ComponentBase
    {
        public bool IsShowRegistration { get; private set; }
        public void MenuSwitch()
        {
            IsShowRegistration = !IsShowRegistration;
        }
    }
}
