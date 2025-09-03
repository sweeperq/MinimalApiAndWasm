using Microsoft.AspNetCore.Identity;

namespace MinimalApiAndWasm.Api.Features.Identity;

public class User : IdentityUser<int>
{
    public User()
    {
    }

    public User(string userName) : base(userName)
    {
    }
}
