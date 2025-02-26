using System.Net;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Enum;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Validators;

public class UserTenantValidationMiddleware(AppDbContext dbContext, IMediator mediator) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userPrincipal = context.User;
        if (context.User.Identity!.IsAuthenticated)
        {
            if (userPrincipal.GetTenantId() == null)
            {
                userPrincipal = await mediator.Send(new AddTenantClaimToUserPrincipalCommand(new UserTenantClaimDto
                {
                    Email = userPrincipal.GetUserEmail(),
                    TenantId = await mediator.Send(new GetTenantIdFromUserQuery(userPrincipal.GetUserEmail()))
                }, userPrincipal));
                await context.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);
            }
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
