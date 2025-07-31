using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Stockly.Api.Configuration.Services.Interfaces;
using Stockly.Core.Models;
using static Stockly.Infrastructure.Adapter.FirebaseDb.Constants.FirebaseConstants;

namespace Stockly.Api.Configuration.Services.Registrars;

public class JwtConfigRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JwtConfig(
            Key: GenerateSecureKey(),
            Issuer: Issuer,
            Audience: Audience,
            ExpiryMinutes: 60
        );

        services.AddSingleton(jwtConfig);

        // Add JWT Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfig.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();
    }

    private static string GenerateSecureKey()
    {
        var keyBytes = new byte[64]; //512 bits
        RandomNumberGenerator.Fill(keyBytes);
        return Convert.ToBase64String(keyBytes)
            .Replace("/", "").Replace("+", "").Replace("=", "");
    }
}