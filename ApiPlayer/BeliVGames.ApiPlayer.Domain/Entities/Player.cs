using System.ComponentModel.DataAnnotations;
using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities;

public class Player:AuditableEntity
{
    public Guid PlayerId { get; set; }
    public EmailAddressAttribute Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentMoney { get; set; }
    public string? LastAccess { get; set; }
    public Guid PlayerDetailId { get; set; }
    public PlayerDetail PlayerDetail { get; set; }
}