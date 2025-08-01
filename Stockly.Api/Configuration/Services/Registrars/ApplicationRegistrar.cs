using Stockly.Api.Configuration.Services.Interfaces;

namespace Stockly.Api.Configuration.Services.Registrars;

public class ApplicationRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));

        // AutoMapper
        services.AddAutoMapper(typeof(ApplicationRegistrar).Assembly);
    }
}