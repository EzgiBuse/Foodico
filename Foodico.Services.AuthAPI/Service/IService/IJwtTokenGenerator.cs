using Microsoft.AspNetCore.Identity;

namespace Foodico.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
