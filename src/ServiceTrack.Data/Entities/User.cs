using Microsoft.AspNetCore.Identity;

namespace ServiceTrack.Data.Entities;

public class User :IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}