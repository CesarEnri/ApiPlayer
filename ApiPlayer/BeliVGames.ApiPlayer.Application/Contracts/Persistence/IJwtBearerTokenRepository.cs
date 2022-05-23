using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Application.Contracts.Persistence;

public interface IJwtBearerTokenRepository: IAsyncRepository<UserRefreshTokens>
{
    
}