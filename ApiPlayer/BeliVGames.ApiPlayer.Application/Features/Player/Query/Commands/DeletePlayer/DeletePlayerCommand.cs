using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.DeletePlayer;

public class DeletePlayerCommand:IRequest
{
    public Guid PlayerId { get; set; }
}