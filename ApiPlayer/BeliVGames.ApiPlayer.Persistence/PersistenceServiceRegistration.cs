using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeliVGames.ApiPlayer.Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BeliVGamesSqlServerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString")!));
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        
        services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BeliVGamesSqlServerDbContext>();

        services.AddScoped<IJwtBearerTokenRepository, JwtBearerTokenRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
    }
}