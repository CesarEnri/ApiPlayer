using BeliVGames.ApiPlayer.Domain.Common;
using BeliVGames.ApiPlayer.Domain.Entities.Desconido;

namespace BeliVGames.ApiPlayer.Domain.Entities;

public class TaxPriceDetail: AuditableEntity
{
    public Guid PriceId { get; set; }
    public Price Price { get; set; }

    public Guid TaxId { get; set; }
    public Tax Tax { get; set; }
}