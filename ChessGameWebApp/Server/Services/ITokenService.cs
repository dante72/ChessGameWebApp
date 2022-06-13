using Models;

namespace ChessGameWebApp.Server.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}