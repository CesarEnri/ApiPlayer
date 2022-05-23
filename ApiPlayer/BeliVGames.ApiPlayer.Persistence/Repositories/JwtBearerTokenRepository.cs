using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;
using Microsoft.AspNetCore.Identity;

namespace BeliVGames.ApiPlayer.Persistence.Repositories;

public class JwtBearerTokenRepository:BaseRepository<UserRefreshTokens>, IJwtBearerTokenRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly BeliVGamesSqlServerDbContext _db;

    public JwtBearerTokenRepository(BeliVGamesSqlServerDbContext dbContext, UserManager<IdentityUser> userManager, BeliVGamesSqlServerDbContext db) : base(dbContext)
    {
        _userManager = userManager;
        _db = dbContext;
    }

    public async Task<bool> IsValidUserAsync(LoginModel users)
    {
        var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Email);
        var result = u != null && await _userManager.CheckPasswordAsync(u, users.Password);
        return result;
    }

    public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
    {
        return _db.UserRefreshToken.SingleOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true) ?? throw new InvalidOperationException();
    }

    public void DeleteUserRefreshTokens(string username, string refreshToken)
    {
        var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
        if (item != null)
        {
            _db.UserRefreshToken.Remove(item);
        }
    }
}