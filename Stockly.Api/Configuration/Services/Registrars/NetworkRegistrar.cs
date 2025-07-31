using Stockly.Api.Configuration.Services.Extensions;
using Stockly.Api.Configuration.Services.Interfaces;

namespace Stockly.Api.Configuration.Services.Registrars;

public class NetworkRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddNetworkServices(configuration);
    }
}