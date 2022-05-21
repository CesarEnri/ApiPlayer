using BeliVGames.ApiPlayer.Api.Models;
using BeliVGames.ApiPlayer.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BeliVGames.ApiPlayer.Persistence;

public class BeliVGamesSqlServerDbContext: DbContext
{
    public BeliVGamesSqlServerDbContext(DbContextOptions<BeliVGamesSqlServerDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
