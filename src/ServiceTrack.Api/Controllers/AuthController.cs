using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Business.DTO;
using ServiceTrack.Business.Services;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class AuthController(AuthService authService) : Controller
{
    [HttpPost("api/v1/Auth/Login")]
    public async Task<ActionResult> Login([FromBody] LoginDto model)
    {
        var userPrincipal = await authService.GetClaimsPrincipalForUser(model);

        await HttpContext.SignInAsync(userPrincipal);

        return NoContent();
    }

    [HttpPost("api/v1/Auth/Register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register(
        [FromBody] RegisterDto model
    )
    {
        await authService.RegisterNewUser(model);
        return NoContent();
    }
}

