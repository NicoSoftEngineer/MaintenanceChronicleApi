using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceTrack.Utilities.Error;
using System.Net;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Enum;
using ServiceTrack.Utilities.Helpers;

namespace ServiceTrack.Application.Validators;

public class UserTenantValidationMiddleware(AppDbContext dbContext) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userPrincipal = context.User;
        if (context.User.Identity.IsAuthenticated)
        {
            //if user has access to tenant continue
            if (!await dbContext.ValidateUserTenantAccess(userPrincipal.GetUserId(), userPrincipal.GetTenantId()))
            {
                //if user does not have access to tenant return forbidden
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorType = ErrorType.UserNotInTenant.ToString(),
                    Error = ErrorType.UserNotInTenant.GetErrorMessage()
                });

                return;
            }
        }

        await next(context);
    }
}
