using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Business.DTO;
using ServiceTrack.Data.Entities;

namespace ServiceTrack.Business.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
{
    public async Task<ClaimsPrincipal> GetClaimsPrincipalForUser(LoginDto model)
    {
        var normalizedEmail = model.Email.ToUpperInvariant();

        var user = await userManager
                .Users
                .SingleOrDefaultAsync(x => x.EmailConfirmed && x.NormalizedEmail == normalizedEmail);
        //TODO: handle if user is null

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        //TODO: Handle signInResult

        var userPrincipal = await signInManager.CreateUserPrincipalAsync(user);

        return userPrincipal;
    }
}