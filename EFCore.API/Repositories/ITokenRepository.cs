using Microsoft.AspNetCore.Identity;

namespace EFCore.API.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
