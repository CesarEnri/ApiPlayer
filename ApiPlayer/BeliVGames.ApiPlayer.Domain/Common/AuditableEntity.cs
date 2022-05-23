namespace BeliVGames.ApiPlayer.Domain.Common;

public class AuditableEntity
{
    protected AuditableEntity()
    {
        CreateDate = DateTime.Now;
    }

    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}