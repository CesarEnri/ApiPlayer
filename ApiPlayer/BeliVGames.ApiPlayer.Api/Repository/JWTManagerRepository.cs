using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BeliVGames.ApiPlayer.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace BeliVGames.ApiPlayer.Api.Repository;

public class JwtManagerRepository: IJwtManagerRepository
{
    private readonly IConfiguration _iConfiguration;
    
    public JwtManagerRepository(IConfiguration iConfiguration)
    {
        this._iConfiguration = iConfiguration;
    }

    public Tokens Authenticate(LoginModel users)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_iConfiguration["JWT:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, users.Email)                    
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Tokens { Token = tokenHandler.WriteToken(token) };
    }

    public Tokens GenerateRefreshToken(string userName)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }
}