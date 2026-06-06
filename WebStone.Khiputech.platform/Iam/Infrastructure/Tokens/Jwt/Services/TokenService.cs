using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebStone.Khiputech.Platform.Iam.Application.Internal.OutboundServices;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Tokens.Jwt.Services;

public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public string GenerateToken(User user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("type", user.Type),
            new Claim("permissions", user.Permissions ?? string.Empty)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return Task.FromResult<int?>(null);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        
            var userIdClaim = principal.FindFirst(ClaimTypes.Sid);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                return Task.FromResult<int?>(userId);
        
            return Task.FromResult<int?>(null);
        }
        catch
        {
            return Task.FromResult<int?>(null);
        }
    }
}