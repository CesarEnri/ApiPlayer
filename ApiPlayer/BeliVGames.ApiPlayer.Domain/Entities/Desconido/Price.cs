using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities.Desconido;

public class Price: AuditableEntity
{
    //public Guid ProductId { get; set; }
    //public Product Product { get; set; }

    public decimal Value { get; set; }
    public decimal Discount { get; set; }

    public int Quantity { get; set; }
}