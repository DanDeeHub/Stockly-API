using Stockly.Api.Configuration.Services.Interfaces;
using Stockly.Core.Interfaces;
using Stockly.Core.Models;
using Stockly.Infrastructure.Adapter.FirebaseDb.Interface;
using Stockly.Infrastructure.Adapter.FirebaseDb.Repositories;
using Stockly.Infrastructure.Adapter.FirebaseDb.Services;

namespace Stockly.Api.Configuration.Services.Registrars;

public class InfrastructureRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Firebase services
        services.AddSingleton<FirestoreService>();
        services.AddSingleton<IFirebaseService, FirebaseService>();
        
        services.AddScoped<IProductService, FirebaseProductService>();
        services.AddScoped<IUserRepository, FirestoreUserRepository>();
    }
}