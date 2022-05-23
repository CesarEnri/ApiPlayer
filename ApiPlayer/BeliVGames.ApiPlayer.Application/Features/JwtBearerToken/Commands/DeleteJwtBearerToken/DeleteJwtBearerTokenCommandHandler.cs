using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.DeleteJwtBearerToken;

public class DeleteJwtBearerTokenCommandHandler: IRequestHandler<DeleteJwtBearerTokenCommand>
{
    private readonly IAsyncRepository<UserRefreshTokens> _jwtBearerTokenRepository;

    public DeleteJwtBearerTokenCommandHandler(IAsyncRepository<UserRefreshTokens> jwtBearerTokenRepository)
    {
        _jwtBearerTokenRepository = jwtBearerTokenRepository;
    }

    public async Task<Unit> Handle(DeleteJwtBearerTokenCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _jwtBearerTokenRepository.GetByIdAsync(request.JwtTokenId);
        await _jwtBearerTokenRepository.DeleteAsync(eventToDelete);

        return Unit.Value;
    }
}