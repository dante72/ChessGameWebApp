using System.Text;

namespace JwtToken;

public class JwtConfig
{
    public string SigningKey { get; set; } = string.Empty;
    public TimeSpan LifeTime { get; set; } = TimeSpan.Zero;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}