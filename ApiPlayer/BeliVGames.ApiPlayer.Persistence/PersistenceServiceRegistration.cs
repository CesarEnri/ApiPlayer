using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeliVGames.ApiPlayer.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {       
        services.AddDbContext<BeliVGamesSqlServerDbContext>();
        //(options =>
        //options.UseSqlServer(configuration.GetConnectionString("ConnectionString")!));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IEventRepository, EventRepository>();
        // services.AddScoped<IOrderRepository, OrderRepository>();

        return services;    
    }
}