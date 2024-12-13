using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class LocationController(IMediator mediator) : ControllerBase
{
}
