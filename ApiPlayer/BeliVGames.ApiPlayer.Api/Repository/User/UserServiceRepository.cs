using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;
using BeliVGames.ApiPlayer.Persistence;
using Microsoft.AspNetCore.Identity;

namespace BeliVGames.ApiPlayer.Api.Repository.User;

public class UserServiceRepository: IUserServiceRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly BeliVGamesSqlServerDbContext _db;
    
    public UserServiceRepository(UserManager<IdentityUser> userManager, BeliVGamesSqlServerDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public async Task<bool> IsValidUserAsync(LoginModel users)
    {
        var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Email);
        var result = await _userManager.CheckPasswordAsync(u, users.Password);
        return result;
    }

    public UserRefreshTokens? AddUserRefreshTokens(UserRefreshTokens? user)
    {
        _db.UserRefreshToken.Add(user);
        return user;
    }

    public void DeleteUserRefreshTokens(string username, string refreshToken)
    {
        var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
        if (item != null)
        {
            _db.UserRefreshToken.Remove(item);
        }
    }
	
    public UserRefreshTokens? GetSavedRefreshTokens(string username, string refreshToken)
    {
        return _db.UserRefreshToken.FirstOrDefault(x => x != null && x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
    }

    public int SaveCommit()
    {
        return _db.SaveChanges();
    }
}