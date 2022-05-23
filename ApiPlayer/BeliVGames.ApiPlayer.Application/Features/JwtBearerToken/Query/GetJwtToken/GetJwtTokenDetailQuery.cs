using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Query.GetJwtToken;

public abstract class GetJwtTokenDetailQuery : IRequest<JwtTokenVm>,  IRequest<List<JwtTokenListVm>>, IRequest<JwtTokenListVm>
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
}