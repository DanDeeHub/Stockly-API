using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Stockly.Api.Configuration.Services.Extensions;

public static class NetworkConfigurationExtensions
{
    public static IServiceCollection AddNetworkServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add CORS for local network
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

        // Configure Kestrel
        services.Configure<KestrelServerOptions>(options =>
        {
            options.ListenAnyIP(5103); // Listen on all interfaces
        });

        return services;
    }

    public static IApplicationBuilder UseNetworkConfiguration(this IApplicationBuilder app)
    {
        app.UseCors("AllowLocalNetwork");
        return app;
    }
}