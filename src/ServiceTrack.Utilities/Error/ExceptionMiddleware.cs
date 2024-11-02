using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ServiceTrack.Utilities.Enum;

namespace ServiceTrack.Utilities.Error;

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
