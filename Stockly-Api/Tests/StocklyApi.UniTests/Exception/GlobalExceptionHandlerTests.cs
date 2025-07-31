using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Stockly.Api.Configuration;

namespace StocklyApi.Tests.UnitTests.Exception;

public class GlobalExceptionHandlerTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task InvokeAsync_ShouldHandleException()
    {
        var context = new DefaultHttpContext();
        var requestDelegate = new RequestDelegate(_ => throw new System.Exception("Test exception"));

        var logger = Mock.Of<ILogger<GlobalExceptionHandler>>();
        var middleware = new GlobalExceptionHandler(requestDelegate, logger);

        await middleware.InvokeAsync(context);

        context.Response.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
    }
}