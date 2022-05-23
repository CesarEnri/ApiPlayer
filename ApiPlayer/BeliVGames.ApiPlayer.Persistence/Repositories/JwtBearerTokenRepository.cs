using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Persistence.Repositories;

public class JwtBearerTokenRepository:BaseRepository<UserRefreshTokens>, IJwtBearerTokenRepository
{
    public JwtBearerTokenRepository(BeliVGamesSqlServerDbContext dbContext) : base(dbContext)
    {
    }
}