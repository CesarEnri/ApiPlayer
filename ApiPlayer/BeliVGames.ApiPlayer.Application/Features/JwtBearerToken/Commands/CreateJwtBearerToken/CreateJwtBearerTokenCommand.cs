using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;

public class CreateJwtBearerTokenCommand:  IRequest<Guid>
{
    public string? UserName { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsActive { get; set; }
}