using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace MinimalApiAndWasm.Api.Features.Identity;

public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole<int>>
{
    public AppUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<IdentityOptions> options) 
        : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Add custom claims here

        return identity;
    }
}
