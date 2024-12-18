using System.Net;
using MaintenanceChronicle.Utilities.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MaintenanceChronicle.Utilities.Error;

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
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                ErrorType = ex.ErrorType.ToString(),
                Error = ex.ErrorType.GetErrorMessage()
            });
        }
        catch (InternalServerException ex)
        {
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
