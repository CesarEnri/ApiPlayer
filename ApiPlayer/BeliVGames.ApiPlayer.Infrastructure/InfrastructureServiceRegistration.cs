using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeliVGames.ApiPlayer.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static void  AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        //services.AddTransient<IEmailService, EmailService>();
        //services.AddTransient<ICsvExporter, CsvExporter>();
        //services.AddSingleton<IJwtManagerRepository, JwtManagerRepository>();
    }
}