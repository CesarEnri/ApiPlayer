using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities;

public class PlayerDetail:AuditableEntity
{
    public Guid PlayerDetailId { get; set; }
    public int QuantityMatches { get; set; }
    public int QuantityKill { get; set; }
    public int QuantityAssist { get; set; }
}