using BeliVGames.ApiPlayer.Api.Models;
using System.Security.Claims;

namespace BeliVGames.ApiPlayer.Api.Repository;

public interface IJwtManagerRepository
{
    Tokens Authenticate(LoginModel users);
    Tokens GenerateRefreshToken(string userName);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}