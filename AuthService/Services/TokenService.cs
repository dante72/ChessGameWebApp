using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;
public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    public TokenService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentException(nameof(jwtConfig));
    }
    public string GenerateToken(Account account)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, account.Email)
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
 
}