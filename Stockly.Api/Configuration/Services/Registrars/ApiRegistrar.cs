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

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();

        // Controllers
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stockly API", Version = "v1" });

            // Add JWT Auth to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}