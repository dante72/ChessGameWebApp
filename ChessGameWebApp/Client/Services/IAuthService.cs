using AuthWebAPI;

namespace ChessGameWebApp.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Autorization(AccountRequestModel account);
    }
}
