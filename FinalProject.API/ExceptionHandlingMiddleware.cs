using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace FinalProject.API;

public class ExceptionHandlingMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"Something went wrong: {exception}");
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { message = "Internal Server Error", details = exception.Message };
        var jsonResponse = JsonSerializer.Serialize(response);

        await httpContext.Response.WriteAsync(jsonResponse);

        return true;
    }
}