using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Stockly.Api.Configuration.Services.Extensions;

public static class NetworkConfigurationExtensions
{
    public static void AddNetworkServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureCors(services);
        ConfigureKestrel(services, configuration);
    }

    private static void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalNetwork", policy =>
            {
                policy.WithOrigins(
                        "http://localhost",
                        "http://127.0.0.1",
                        "http://0.0.0.0",
                        "http://[::]",
                        "http://*.local")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
            });
        });
    }

    private static void ConfigureKestrel(IServiceCollection services, IConfiguration configuration)
    {
        // Get ports from config with defaults
        var mainPort = configuration.GetValue("Kestrel:Ports:Main", 5103);
        var metricsPort = configuration.GetValue("Kestrel:Ports:Metrics", 0); // 0 = disabled

        services.Configure<KestrelServerOptions>(options =>
        {
            options.ListenAnyIP(mainPort);

            // Add metrics endpoint if configured
            if (metricsPort > 0 && metricsPort != mainPort)
            {
                options.ListenAnyIP(metricsPort);
                configuration["Metrics:Endpoint"] = $":{metricsPort}";
            }
        });
    }

    public static void UseNetworkConfiguration(this IApplicationBuilder app)
    {
        app.UseCors("AllowLocalNetwork");
    }
}