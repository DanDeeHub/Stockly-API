using Microsoft.OpenApi.Models;
using Stockly.Api.Configuration.Services.Interfaces;
using Stockly.Api.Services;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Adapter.FirebaseDb.Services;

namespace Stockly.Api.Configuration.Services.Registrars;

public class ApiRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // API Services
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IAuthService, FirebaseAuthService>();

        // Controllers
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Stockly API",
                Version = "v1",
                Description = "Stockly API Documentation"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
        });
    }
}