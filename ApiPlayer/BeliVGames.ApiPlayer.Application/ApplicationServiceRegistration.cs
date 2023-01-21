using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BeliVGames.ApiPlayer.Application;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}