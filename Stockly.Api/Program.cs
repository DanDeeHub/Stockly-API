using Stockly.Api.Configuration;
using Stockly.Api.Configuration.Services.Extensions;
using Stockly.Api.Configuration.Services.Registrars;

var builder = WebApplication.CreateBuilder(args);

// Register all services
builder.Services.AddStocklyServices(
    builder.Configuration,
    typeof(ApiRegistrar).Assembly //Core and Infrastructure registrations
);

var app = builder.Build();
app.UseStocklyConfiguration();
await app.RunAsync();

public abstract partial class Program;
