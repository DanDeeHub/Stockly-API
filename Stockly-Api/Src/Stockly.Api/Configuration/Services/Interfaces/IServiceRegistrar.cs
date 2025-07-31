namespace Stockly.Api.Configuration.Services.Interfaces;

public interface IServiceRegistrar
{
    void RegisterServices(IServiceCollection services, IConfiguration configuration);
}