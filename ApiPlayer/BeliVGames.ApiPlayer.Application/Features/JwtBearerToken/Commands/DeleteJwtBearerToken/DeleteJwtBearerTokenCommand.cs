using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.DeleteJwtBearerToken;

public class DeleteJwtBearerTokenCommand: IRequest
{
    public Guid JwtTokenId { get; set; }
}