using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;

namespace BeliVGames.ApiPlayer.Api.Repository.User;

public interface IUserServiceRepository
{
    Task<bool> IsValidUserAsync(LoginModel users);
	
    UserRefreshTokens? AddUserRefreshTokens(UserRefreshTokens? user);

    UserRefreshTokens? GetSavedRefreshTokens(string username, string refreshToken);

    void DeleteUserRefreshTokens(string username, string refreshToken);

    int SaveCommit();
}