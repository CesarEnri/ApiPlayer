using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.Inventory;

public class Category: AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}