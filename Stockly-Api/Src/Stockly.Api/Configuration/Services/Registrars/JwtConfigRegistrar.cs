using System.Security.Cryptography;
using Stockly.Api.Configuration.Services.Interfaces;
using Stockly.Core.Models;
using static Stockly.Api.Constants.FirebaseConstants;

namespace Stockly.Api.Configuration.Services.Registrars;

public class JwtConfigRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new JwtConfig(
            Key: GenerateSecureKey(),
            Issuer: Issuer,
            Audience: Audience,
            ExpiryMinutes: 60
        ));
    }

    private static string GenerateSecureKey()
    {
        var keyBytes = new byte[64]; //512 bits
        RandomNumberGenerator.Fill(keyBytes);
        return Convert.ToBase64String(keyBytes)
            .Replace("/", "").Replace("+", "").Replace("=", "");
    }
}