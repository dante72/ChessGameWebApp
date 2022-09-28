using AuthWebAPI;

namespace ChessGameWebApp.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Autorization(AccountRequestModel account);
        Task<bool> TokenAutorization();
        Task LogOut();
    }
}
