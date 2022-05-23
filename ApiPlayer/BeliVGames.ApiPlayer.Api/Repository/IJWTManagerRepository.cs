using System.Security.Claims;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;

namespace BeliVGames.ApiPlayer.Api.Repository;

public interface IJwtManagerRepository
{
    Tokens Authenticate(LoginModel users);
    string GenerateRefreshToken(string userName);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}