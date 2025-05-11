using System.Net;
using MaintenanceChronicle.Utilities.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace MaintenanceChronicle.Utilities.Error;

/// <summary>
/// Middleware to handle exceptions.
/// </summary>
/// <param name="logger">App logger</param>
/// <param name="next">Next request</param>
public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            logger.LogError(ex, ex.Message);
            var errorResponse = new
            {
                Errors = new Dictionary<string, string[]>
                {
                    { ex.PropertyName ?? "General", new[] { ex.ErrorType.GetErrorMessage() } }
                }
            };
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (UnauthorizedRequestException ex)
        {
            logger.LogError(ex, ex.Message);
            var errorResponse = new { Message = ex.ErrorType.GetErrorMessage() };
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (InternalServerException ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Errors = ex.Errors
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = ex.Message
            });
        }
    }
}
