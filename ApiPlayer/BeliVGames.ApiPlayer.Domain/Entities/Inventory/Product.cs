using BeliVGames.ApiPlayer.Domain.Common;
using BeliVGames.ApiPlayer.Domain.Entities.Desconido;

namespace BeliVGames.ApiPlayer.Domain.Entities.Inventory;

public class Product: AuditableEntity
{
    public Guid SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
    
    public Guid VendorId { get; set; }
    public Vendor Vendor { get; set; }
    
    public Guid PriceId { get; set; }
    public Price Price { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }

    public bool Sold { get; set; }
}