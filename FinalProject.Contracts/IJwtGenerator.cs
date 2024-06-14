using Microsoft.AspNetCore.Identity;

namespace FinalProject.Contracts
{
    public interface IJwtGenerator
    {
        string GenerateToken(IdentityUser applicationUser, IEnumerable<string> roles);

    }
}
