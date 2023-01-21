using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeliVGames.ApiPlayer.Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("SqlServerDataBase")));
        
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("PostgreSqlDataBase")));
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        //services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddScoped<IJwtBearerTokenRepository, JwtBearerTokenRepository>();
        //services.AddScoped<IPlayerRepository, PlayerRepository>();
    }
}