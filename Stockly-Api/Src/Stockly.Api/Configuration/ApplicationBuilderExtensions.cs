using Stockly.Api.Configuration.Services.Extensions;

namespace Stockly.Api.Configuration;

public static class ApplicationBuilderExtensions
{
    public static void UseStocklyConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stockly API v1");
                c.RoutePrefix = string.Empty;

                // Optional: Add OAuth config here if needed
                // c.OAuthClientId("swagger-ui");
                // c.OAuthAppName("Swagger UI");
            });
        }

        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseNetworkConfiguration();
    }
}