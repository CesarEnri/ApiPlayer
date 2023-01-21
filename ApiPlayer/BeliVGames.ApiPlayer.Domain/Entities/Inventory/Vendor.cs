using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.Inventory;

public class Vendor: AuditableEntity
{
    //Foreign Key from Name Master coming soon!!.
    public string Name { get; set; }//TODO a evaluar luego de crear el Name Master
    public string Description { get; set; }//TODO a evaluar luego de crear el Name Master
}