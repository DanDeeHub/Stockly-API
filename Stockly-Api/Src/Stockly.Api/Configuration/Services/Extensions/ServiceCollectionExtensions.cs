using System.Reflection;
using Stockly.Api.Configuration.Services.Interfaces;

namespace Stockly.Api.Configuration.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddStocklyServices(this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var registrars = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IServiceRegistrar).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IServiceRegistrar>();

        foreach (var registrar in registrars)
        {
            registrar.RegisterServices(services, configuration);
        }
    }
}