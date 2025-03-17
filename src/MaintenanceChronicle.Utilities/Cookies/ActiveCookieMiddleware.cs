using MaintenanceChronicle.Utilities.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Enum;
using MaintenanceChronicle.Utilities.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MaintenanceChronicle.Utilities.Cookies;

public class ActiveCookieMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var activeCookieKey = context.Request.Cookies
            .FirstOrDefault(c => c.Key == MaintenanceChronicleClaimTypes.ActiveCookieName).Value;
        var activeCookie = context.Request.Cookies
            .FirstOrDefault(c => c.Key == activeCookieKey).Value;

        var cookieToAdd =
            new KeyValuePair<string, string>(CookieAuthenticationDefaults.AuthenticationScheme, activeCookie);
         context.Request.Cookies = (IRequestCookieCollection)context.Request.Cookies.Append(cookieToAdd);

        var r = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        await next(context);
    }
}
