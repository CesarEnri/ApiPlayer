using BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerList;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerDetail;

public class GetPlayerDetailQuery: IRequest<PlayerListVm>, IRequest<List<PlayerListVm>>, IRequest<PlayerDetailVm>
{
    public Guid Id { get; set; }
}