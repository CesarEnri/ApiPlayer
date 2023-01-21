using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities;

public class Tax: AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
}