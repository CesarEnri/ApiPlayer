using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.UpdatePlayer;

public class UpdatePlayerCommand: IRequest
{
    public Guid PlayerId { get; set; }
    public EmailAddressAttribute Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentMoney { get; set; }
    public string? LastAccess { get; set; }
    public Guid PlayerDetailId { get; set; }
}