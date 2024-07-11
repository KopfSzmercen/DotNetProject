using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.Cors;

internal static class CorsExtensions
{
    private const string CorsPolicy = "CorsPolicy";

    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration.GetOptions<CorsOptions>(CorsOptions.SectionName);

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy,
                builder => builder
                    .WithOrigins(corsOptions.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        return services;
    }

    public static void UseCors(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
    }
}