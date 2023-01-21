namespace BeliVGames.ApiPlayer.Domain.Common;

public class AuditableEntity
{
    public Guid Id { get; set; }
    
    protected AuditableEntity()
    {
        CreateAt = DateTime.Now;
    }


    public bool Active { get; set; }
    public DateTime CreateAt{ get; set; }
    public Guid CreateBy { get; set; }
    
    public DateTime? UpdateAt { get; set; }
    public Guid? UpdateBy { get; set; }
}