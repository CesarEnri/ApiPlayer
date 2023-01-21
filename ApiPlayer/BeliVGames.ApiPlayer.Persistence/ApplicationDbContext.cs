using BeliVGames.ApiPlayer.Domain.Common;
using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Entities.Desconido;
using BeliVGames.ApiPlayer.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;

namespace BeliVGames.ApiPlayer.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext()
    {
        
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<ComplementTable>? ComplementTables { get; set; }
    public DbSet<Brand>? Brands { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Quality>? Qualities { get; set; }
    public DbSet<SubCategory>? SubCategories { get; set; }
    public DbSet<Vendor>? Vendors { get; set; }
    public DbSet<Price>? Prices { get; set; }
    public DbSet<Tax>? Taxes { get; set; }
    public DbSet<TaxPriceDetail>? TaxPriceDetails { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql("connecting string");
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateAt = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}