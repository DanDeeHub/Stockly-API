using Google.Cloud.Firestore;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Adapter.FirebaseDb.Interface;
using Stockly.Infrastructure.Adapter.FirebaseDb.Repositories;
using Stockly.Infrastructure.Adapter.FirebaseDb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
    .AddSingleton<FirestoreDb>(/* config */)
    .AddScoped<IAuthService, FirebaseAuthService>()
    .AddScoped<IUserRepository, FirestoreUserRepository>();

builder.Services.AddSingleton<IFirebaseService, FirebaseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();
