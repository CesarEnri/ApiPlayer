using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.Inventory;

public class Brand: AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}