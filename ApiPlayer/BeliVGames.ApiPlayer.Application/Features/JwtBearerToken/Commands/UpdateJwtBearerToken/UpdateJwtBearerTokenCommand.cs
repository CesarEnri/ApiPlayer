using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.UpdateJwtBearerToken;

public class UpdateJwtBearerTokenCommand: IRequest
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; }
    public bool IsActive { get; set; } = true;
}