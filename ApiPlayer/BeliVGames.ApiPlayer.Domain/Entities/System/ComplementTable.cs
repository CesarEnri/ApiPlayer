using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.System;

public class ComplementTable: AuditableEntity
{
    public Guid ReferenceGuid { get; set; }//TODO investigar si el guid se crea automatico y no es posible pasarlo.
    public string TableReference { get; set; }
    public string NameColumn { get; set; }
    public string TypeColumn { get; set; }
    public string ValueColumn { get; set; }
}
