using Models;

namespace AuthService.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}