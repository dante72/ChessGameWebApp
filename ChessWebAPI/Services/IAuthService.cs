using AuthWebAPI;
using AuthWebAPI.AuthWebAPI;

namespace AuthWebAPI.Services
{
    public interface IAuthService
    {
        Task<bool> Autorization(AccountRequestModel account);
        Task<bool> TokenAutorization();
        Task LogOut();
    }
}
