using Models;

namespace JwtToken;

public interface ITokenService
{
    string GenerateToken(Account account);
}