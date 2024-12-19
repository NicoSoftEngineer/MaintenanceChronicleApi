using MaintenanceChronicle.Utilities.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceChronicle.Api.Controllers;

//Makes endpoints accessible only for users with Admin or GlobalAdmin roles
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin}")]
[ApiController]
public class MachineController(IMediator mediator) : ControllerBase
{
    [HttpPost("/api/v1/machines")]
    public async Task CreateMachine([FromBody] object newMachineDetail)
    {

    }
}
