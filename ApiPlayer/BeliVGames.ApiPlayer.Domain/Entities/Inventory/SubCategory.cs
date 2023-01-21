using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.Inventory;

public class SubCategory: AuditableEntity
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}