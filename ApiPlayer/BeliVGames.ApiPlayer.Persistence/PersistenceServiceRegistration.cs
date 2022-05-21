using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BeliVGames.ApiPlayer.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {       
        services.AddDbContext<BeliVGamesSqlServerDbContext>();
        //(options =>
        //options.UseSqlServer(configuration.GetConnectionString("ConnectionString")!));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        
        services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BeliVGamesSqlServerDbContext>();
        
        //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)//Por ahora
        //    .AddEntityFrameworkStores<BeliVGamesSqlServerDbContext>();

        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IEventRepository, EventRepository>();
        // services.AddScoped<IOrderRepository, OrderRepository>();

        return services;    
    }
}