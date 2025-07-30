using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Stockly.Api.Controllers;
using Stockly.Api.Services;
using Stockly.Core.Interfaces;
using Stockly.Core.Models;
using Stockly.Infrastructure.Adapter.FirebaseDb.Interface;
using Stockly.Infrastructure.Adapter.FirebaseDb.Repositories;
using Stockly.Infrastructure.Adapter.FirebaseDb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Configure JWT settings
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UsersController).Assembly));

// Services
builder.Services
    .AddSingleton<FirestoreService>()
    .AddSingleton<IFirebaseService, FirebaseService>()
    .AddScoped<ITokenService, JwtTokenService>(provider => 
    {
        var config = provider.GetRequiredService<IOptions<JwtConfig>>().Value;
        return new JwtTokenService(config);
    })
    .AddScoped<IAuthService, FirebaseAuthService>()
    .AddScoped<IUserRepository, FirestoreUserRepository>();

// Add Controllers (required for MapControllers)
builder.Services.AddControllers();

builder.Services.AddAuthorization();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stockly API", Version = "v1" });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stockly API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
