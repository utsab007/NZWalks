using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string GenerateJwtTokenAsync(IdentityUser user,List<string> roles);
    }
}
