using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Homework_19.Data;
using Microsoft.IdentityModel.Tokens;

namespace Homework_19.Helpers;

public class GenerateToken
{
    public static string GetToken(User user, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}