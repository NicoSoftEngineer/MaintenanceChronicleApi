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
    public async Task<ActionResult> Register(
        [FromBody] RegisterDto model
    )
    {
        await authService.RegisterNewUser(model);
        return NoContent();
    }

    /// <summary>
    /// unescape token before sending
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/ValidateToken")]
    public async Task<ActionResult> ValidateToken(
        [FromBody] TokenDto model
    )
    {
        await authService.ValidateToken(model);

        return NoContent();
    }
}

