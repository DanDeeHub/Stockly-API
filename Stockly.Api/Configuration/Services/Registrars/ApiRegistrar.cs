using Stockly.Api.Configuration.Services.Interfaces;
using Stockly.Api.Services;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Adapter.FirebaseDb.Services;

namespace Stockly.Api.Configuration.Services.Registrars;

public class ApiRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Core API Services
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IAuthService, FirebaseAuthService>();
        services.AddHttpContextAccessor();
        services.AddControllers();
    }
}