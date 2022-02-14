using System.ComponentModel.DataAnnotations;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerList;

public class PlayerListVm
{
    public Guid PlayerId { get; set; }
    public EmailAddressAttribute Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentMoney { get; set; }
    public string? LastAccess { get; set; }
}