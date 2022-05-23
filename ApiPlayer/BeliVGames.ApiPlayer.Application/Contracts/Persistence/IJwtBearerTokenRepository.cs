using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;

namespace BeliVGames.ApiPlayer.Application.Contracts.Persistence;

public interface IJwtBearerTokenRepository: IAsyncRepository<UserRefreshTokens>
{
    Task<bool> IsValidUserAsync(LoginModel users);
    UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken);
    void DeleteUserRefreshTokens(string username, string refreshToken);
}